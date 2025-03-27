namespace FlowerRename
{
    public abstract class RuleBase : UserControl, IRenameRule
    {
        protected Panel ExpandedPanel;
        protected bool _isExpanded;
        protected bool _isEnabled; 
        public abstract string RuleName { get; }
        public bool IsExpanded 
        { 
            get => _isExpanded;
            set => _isExpanded = value;
        }
        public virtual bool IsEnabled 
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }
        public RuleBase()
        {
            InitializeExpandedPanel();
        }

        protected virtual void InitializeExpandedPanel()
        {
            ExpandedPanel = new Panel
            {
                Visible = false
            };
            _isExpanded = false;
            _isEnabled = true; 
        }

        public virtual void ToggleExpand()
        {
            _isExpanded = !_isExpanded;
            ExpandedPanel.Visible = _isExpanded;
        }

        // { IRenameRule  Apply 中文
        public abstract string[] Apply(string[] fileNames);

        public void UpdateParameters()
        {
        }
        // { IRenameRule  GetNewName
        public abstract string GetNewName(string originalName);

        // IRenameRule 
        public abstract string[] GenerateFileName(string[] originalFileNames);
        public abstract string ProcessFileName(string originalName);
        public abstract bool Validate();
        public virtual UserControl GetConfigControl() => this;
    }
}