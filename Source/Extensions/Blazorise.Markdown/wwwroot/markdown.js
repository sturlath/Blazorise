const _instances = [];

export function initialize(dotNetObjectRef, element, elementId, options) {
    const instances = _instances;

    if (!options.toolbar) {
        // remove empty toolbar so that we can fallback to the default items
        delete options.toolbar;
    }
    else if (options.toolbar && options.toolbar.length > 0) {
        // map any named action with a real action from EasyMDE
        options.toolbar.forEach(button => {
            // make sure we don't operate on separators
            if (button !== "|") {
                if (button.action) {
                    if (!button.action.startsWith("http")) {
                        button.action = EasyMDE[button.action];
                    }
                }
                else {
                    if (button.name && button.name.startsWith("http")) {
                        button.action = button.name;
                    }
                    else {
                        // custom action is used so we need to trigger custom event on click
                        button.action = (editor) => {
                            dotNetObjectRef.invokeMethodAsync('NotifyCustomButtonClicked', button.name, button.value).then(null, function (err) {
                                throw new Error(err);
                            });
                        }
                    }
                }

                // button without icon is not allowed
                if (!button.className) {
                    button.className = "fa fa-question";
                }
            }
        });
    }

    let nextFileId = 0;
    let imageUploadNotifier = {
        onSuccess: (e) => { },
        onError: (e) => { }
    };

    const easyMDE = new EasyMDE({
        element: document.getElementById(elementId),
        hideIcons: options.hideIcons,
        showIcons: options.showIcons,
        renderingConfig: {
            singleLineBreaks: false,
            codeSyntaxHighlighting: true
        },
        initialValue: options.value,
        sideBySideFullscreen: false,
        autoDownloadFontAwesome: options.autoDownloadFontAwesome,
        lineNumbers: options.lineNumbers,
        lineWrapping: options.lineWrapping,
        minHeight: options.minHeight,
        maxHeight: options.maxHeight,
        placeholder: options.placeholder,
        tabSize: options.tabSize,
        theme: options.theme,
        direction: options.direction,
        toolbar: options.toolbar,
        toolbarTips: options.toolbarTips,

        uploadImage: true,
        imageMaxSize: options.imageMaxSize,
        imageAccept: options.imageAccept,
        imageUploadEndpoint: options.imageUploadEndpoint,
        imagePathAbsolute: options.imagePathAbsolute,
        imageCSRFToken: options.imageCSRFToken,
        imageTexts: options.imageTexts,
        imageUploadFunction: (file, onSuccess, onError) => {
            // hack to save the reference to the callback functions
            imageUploadNotifier.onSuccess = onSuccess;
            imageUploadNotifier.onError = onError;

            // Reduce to purely serializable data, plus build an index by ID
            element._blazorFilesById = {};

            var fileEntry = {
                id: ++nextFileId,
                lastModified: new Date(file.lastModified).toISOString(),
                name: file.name,
                size: file.size,
                type: file.type
            };

            element._blazorFilesById[fileEntry.id] = fileEntry;

            // Attach the blob data itself as a non-enumerable property so it doesn't appear in the JSON
            Object.defineProperty(fileEntry, 'blob', { value: file });

            dotNetObjectRef.invokeMethodAsync('NotifyImageUpload', fileEntry).then(null, function (err) {
                throw new Error(err);
            });
        },

        errorMessages: options.errorMessages,
        errorCallback: (errorMessage) => {
            dotNetObjectRef.invokeMethodAsync("NotifyErrorMessage", errorMessage);
        }
    });

    easyMDE.codemirror.on("change", function () {
        dotNetObjectRef.invokeMethodAsync("UpdateInternalValue", easyMDE.value());
    });

    instances[elementId] = {
        dotNetObjectRef: dotNetObjectRef,
        elementId: elementId,
        editor: easyMDE,
        imageUploadNotifier: imageUploadNotifier
    };
}

export function destroy(element, elementId) {
    const instances = _instances || {};
    delete instances[elementId];
}

export function setValue(elementId, value) {
    const instance = _instances[elementId];

    if (instance) {
        instance.editor.value(value);
    }
}

export function getValue(elementId) {
    const instance = _instances[elementId];

    if (instance) {
        return instance.editor.value();
    }

    return null;
}

export function notifyImageUploadSuccess(elementId, imageUrl) {
    const instance = _instances[elementId];

    if (instance) {
        return instance.imageUploadNotifier.onSuccess(imageUrl);
    }
}

export function notifyImageUploadError(elementId, errorMessage) {
    const instance = _instances[elementId];

    if (instance) {
        return instance.imageUploadNotifier.onError(errorMessage);
    }
}