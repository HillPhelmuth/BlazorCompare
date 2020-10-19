using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TestPerform5proto.Client.Pages
{
    public partial class TestInterop
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        [Inject]
        public IJSInProcessRuntime JsInProcessRuntime { get; set; }

        private string _elapsedMsJsRuntime;
        private string _elapsedMsJsProcess;
        private bool _jsRuntimeBusy;
        private bool _jsProcessBusy;
        private int _jsRuntimeInput = 1000;
        private int _jsProcessInput = 1000;

        private void UseJs()
        {
            _jsRuntimeBusy = true;
            InvokeAsync(StateHasChanged);
            _ = UseJsRuntime();
        }
        private async Task UseJsRuntime()
        {
            int val = 0;
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < _jsRuntimeInput; i++)
            {
                val = await JsRuntime.InvokeAsync<int>("plusOne", val);
            }
            sw.Stop();
            _elapsedMsJsRuntime = $"{sw.ElapsedMilliseconds}ms";
            _jsRuntimeBusy = false;
            await InvokeAsync(StateHasChanged);
        }

        private void UseJsInProcessRuntime()
        {
            _jsProcessBusy = true;
            InvokeAsync(StateHasChanged);
            int val = 0;
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < _jsProcessInput; i++)
            {
                val = JsInProcessRuntime.Invoke<int>("plusOne", val);
            }
            sw.Stop();
            _elapsedMsJsProcess = $"{sw.ElapsedMilliseconds}ms";
            _jsProcessBusy = false;
            StateHasChanged();
        }
    }
}
