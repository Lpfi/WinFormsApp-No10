namespace WinFormsApp_No10
{
    public partial class QuizChoiceControl : UserControl
    {
        public QuizChoiceControl()
        {
            InitializeComponent();
        }

        public string QuestionText
        {
            get { return lblQuestion.Text; }
            set { lblQuestion.Text = value; }
        }

        public string Choice1Text
        {
            get { return radioButton1.Text; }
            set { radioButton1.Text = value; }
        }

        public string Choice2Text
        {
            get { return radioButton2.Text; }
            set { radioButton2.Text = value; }
        }

        public string Choice3Text
        {
            get { return radioButton3.Text; }
            set { radioButton3.Text = value; }
        }

        public string Choice4Text
        {
            get { return radioButton4.Text; }
            set { radioButton4.Text = value; }
        }

        public int SelectedChoiceIndex()
        {
            if (radioButton1.Checked) return 0;
            if (radioButton2.Checked) return 1;
            if (radioButton3.Checked) return 2;
            if (radioButton4.Checked) return 3;
            return -1; 
        }

        public void SetSelectedChoice(int index)
        {
            switch (index)
            {
                case 0: radioButton1.Checked = true; break;
                case 1: radioButton2.Checked = true; break;
                case 2: radioButton3.Checked = true; break;
                case 3: radioButton4.Checked = true; break;
                default: 
                    radioButton1.Checked = radioButton2.Checked =
                    radioButton3.Checked = radioButton4.Checked = false;
                    break;
            }
        }
    }
}
