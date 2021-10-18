﻿#region Using directives
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
#endregion

namespace Blazorise.Modules
{
    /// <summary>
    /// Contracts for the <see cref="FileEdit"/> JS module.
    /// </summary>
    public interface IJSFileEditModule : IBaseJSModule,
        IJSDestroyableModule
    {
        /// <summary>
        /// Initializes the new <see cref="FileEdit"/> within the JS module.
        /// </summary>
        /// <param name="dotNetObjectRef"></param>
        /// <param name="elementRef">Reference to the rendered element.</param>
        /// <param name="elementId">ID of the rendered element.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        ValueTask Initialize( DotNetObjectReference<FileEditAdapter> dotNetObjectRef, ElementReference elementRef, string elementId );

        /// <summary>
        /// Reads the batch of data for the specified position and offset.
        /// </summary>
        /// <param name="elementRef">Reference to the rendered element.</param>
        /// <param name="fileEntryId">File id.</param>
        /// <param name="position">Byte position starting from zero.</param>
        /// <param name="length">Number of bytes to read.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        ValueTask<string> ReadDataAsync( ElementReference elementRef, int fileEntryId, long position, long length, CancellationToken cancellationToken = default );

        /// <summary>
        /// Resets the file input values.
        /// </summary>
        /// <param name="elementRef">Reference to the rendered element.</param>
        /// <param name="elementId">ID of the rendered element.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        ValueTask Reset( ElementReference elementRef, string elementId );

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        /// <param name="elementRef">Reference to the rendered element.</param>
        /// <param name="elementId">ID of the rendered element.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        ValueTask OpenFileDialog( ElementReference elementRef, string elementId );
    }
}
