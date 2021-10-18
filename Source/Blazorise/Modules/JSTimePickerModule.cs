﻿#region Using directives
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
#endregion

namespace Blazorise.Modules
{
    /// <summary>
    /// Default implementation of the <see cref="TimePicker{TValue}"/> JS module.
    /// </summary>
    public class JSTimePickerModule : BaseJSModule, IJSTimePickerModule
    {
        #region Constructors

        /// <summary>
        /// Default module constructor.
        /// </summary>
        /// <param name="jsRuntime">JavaScript runtime instance.</param>
        public JSTimePickerModule( IJSRuntime jsRuntime )
            : base( jsRuntime )
        {
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public virtual async ValueTask Initialize( ElementReference elementRef, string elementId, object options )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "initialize", elementRef, elementId, options );
        }

        /// <inheritdoc/>
        public virtual async ValueTask Destroy( ElementReference elementRef, string elementId )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "destroy", elementRef, elementId );
        }

        /// <inheritdoc/>
        public virtual ValueTask Activate( ElementReference elementRef, string elementId, object options )
        {
            return ValueTask.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual async ValueTask UpdateValue( ElementReference elementRef, string elementId, object value )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "updateValue", elementRef, elementId, value );
        }

        /// <inheritdoc/>
        public virtual async ValueTask UpdateOptions( ElementReference elementRef, string elementId, object options )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "updateOptions", elementRef, elementId, options );
        }

        /// <inheritdoc/>
        public virtual async ValueTask Open( ElementReference elementRef, string elementId )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "open", elementRef, elementId );
        }

        /// <inheritdoc/>
        public virtual async ValueTask Close( ElementReference elementRef, string elementId )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "close", elementRef, elementId );
        }

        /// <inheritdoc/>
        public virtual async ValueTask Toggle( ElementReference elementRef, string elementId )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "toggle", elementRef, elementId );
        }

        /// <inheritdoc/>
        public virtual async ValueTask Focus( ElementReference elementRef, string elementId, bool scrollToElement )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "focus", elementRef, elementId, scrollToElement );
        }

        /// <inheritdoc/>
        public virtual async ValueTask Select( ElementReference elementRef, string elementId, bool focus )
        {
            var moduleInstance = await Module;

            await moduleInstance.InvokeVoidAsync( "select", elementRef, elementId, focus );
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public override string ModuleFileName => "./_content/Blazorise/timePicker.js";

        #endregion
    }
}
