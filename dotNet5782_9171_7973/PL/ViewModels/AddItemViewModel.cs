using PO;

namespace PL.ViewModels
{
    /// <summary>
    /// An abstarct class to represent a view model for add an entity view model
    /// </summary>
    /// <typeparam name="T">Type of the entity to add</typeparam>
    abstract class AddItemViewModel<T> where T : PropertyChangedNotification, new()
    {
        /// <summary>
        /// The model - entity to add
        /// </summary>
        public T Model { get; set; } = new();

        /// <summary>
        /// Command to add an item
        /// </summary>
        public RelayCommand<object> AddCommand { get; set; }

        /// <summary>
        /// Constructor
        /// initialize command
        /// </summary>
        public AddItemViewModel()
        {
            AddCommand = new RelayCommand<object>(ExecuteAdd, param => Model.Error == null);
        }

        /// <summary>
        /// Add the item 
        /// </summary>
        /// <param name="target"></param>
        private void ExecuteAdd(object target)
        {
            try
            {
                Add();

                string name = typeof(T).Name.Replace("ToAdd", "");
                MessageBox.Show(MessageBox.BoxType.Success, $"{name} added successfully");
                Workspace.RemovePanelCommand.Execute($"Add {name}");
            }
            catch (BO.IdAlreadyExistsException e)
            {
                MessageBox.Show(MessageBox.BoxType.Error, e.Message);
            }
        }

        protected abstract void Add();
    }
}
