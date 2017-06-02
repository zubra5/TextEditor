using System;
using TextEditor.BL;

namespace TextEditor
{
    public class MainPresenter
    {
        private readonly IMainForm _view;
        private readonly IFileManager<string> _manager;
        private readonly IMessageService _messageService;
        private string _currentFilePath;

        public MainPresenter(IMainForm view, IFileManager<string> manager, IMessageService service) 
        {
            _view = view;
            _manager = manager;
            _messageService = service;

            _view.SetSymbolCount(0);
            _view.ContentChanged += _view_ContentChanged;
            _view.FileOpenClick += _view_FileOpenClick;
            _view.FileSaveClick += _view_FileSaveClick;
        }

        void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;
                _manager.SaveContent(content, _currentFilePath);
                _messageService.ShowMessage("Файл успешно сохранен");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        void _view_FileOpenClick(object sender, System.EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;
                bool isExist = _manager.IsExist(filePath);
                if (!isExist) 
                { 
                    _messageService.ShowExclamation("Выбранный файл не существует.");
                    return;
                }

                _currentFilePath = filePath;

                string content = _manager.GetContent(filePath);
                int count = _manager.GetSymbolCount(content);

                _view.Content = content;
                _view.SetSymbolCount(count);
            }
            catch (Exception ex) 
            {
                _messageService.ShowError(ex.Message);
            }
        }

        void _view_ContentChanged(object sender, System.EventArgs e)
        {
            string content = _view.Content;

            int count = _manager.GetSymbolCount(content);

            _view.SetSymbolCount(count);
        } 
    }
}
