using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwayCCP.ViewModels
{
    class DefaultDialogViewModel : BindableBase, IDialogAware
    {
        public DialogCloseListener RequestClose { get; }

        private string? content;
        public string? Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            // do nothing
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var content = parameters.GetValue<string>("content");
            Content = content;
        }

        public void OK()
        {
            RequestClose.Invoke();
        }
    }
}
