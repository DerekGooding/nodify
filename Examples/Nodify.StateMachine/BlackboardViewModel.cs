using Nodify.Shared;
using System.Linq;

namespace Nodify.StateMachine
{
    public class BlackboardViewModel : ObservableObject
    {
        private NodifyObservableCollection<BlackboardKeyViewModel> _keys = new();
        public NodifyObservableCollection<BlackboardKeyViewModel> Keys
        {
            get => _keys;
            set => SetProperty(ref _keys, value);
        }

        private NodifyObservableCollection<BlackboardItemReferenceViewModel> _actions = new();
        public NodifyObservableCollection<BlackboardItemReferenceViewModel> Actions
        {
            get => _actions;
            set => SetProperty(ref _actions, value);
        }

        private NodifyObservableCollection<BlackboardItemReferenceViewModel> _conditions = new();
        public NodifyObservableCollection<BlackboardItemReferenceViewModel> Conditions
        {
            get => _conditions;
            set => SetProperty(ref _conditions, value);
        }

        public INodifyCommand AddKeyCommand { get; }
        public INodifyCommand RemoveKeyCommand { get; }

        public BlackboardViewModel()
        {
            AddKeyCommand = new DelegateCommand(() => Keys.Add(new BlackboardKeyViewModel
            {
                Name = "New Key "
            }));

            RemoveKeyCommand = new DelegateCommand<BlackboardKeyViewModel>(key => Keys.Remove(key));

            Keys.WhenAdded(key =>
            {
                var existingKeyNames = Keys.Where(k => k != key).Select(k => k.Name).ToList();
                key.Name = existingKeyNames.GetUnique(key.Name);
            });
        }
    }
}
