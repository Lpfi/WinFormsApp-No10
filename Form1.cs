using WinFormsApp_No10.Models;
using Newtonsoft.Json;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp_No10
{
    public partial class Form1 : Form
    {
        private List<Question> questionsCache = new List<Question>(); 
        private DateTime lastUpdatedTimestamp = DateTime.MinValue; 
        private Timer timer; 
        private int timeElapsed = 0; 
        private bool examStarted = false; 
        private int currentQuestionIndex = 0; 

        private List<Question> answeredQuestions = new List<Question>();

        // Test database connection
        private void TestDatabaseConnection()
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["QuizDBConnectionString"].ConnectionString))
            //    {
            //        conn.Open();
            //        MessageBox.Show("เชื่อมต่อฐานข้อมูล ExamNo10 สำเร็จ!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("การเชื่อมต่อฐานข้อมูลล้มเหลว: " + ex.Message);
            //}
        }

        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["QuizDBConnectionString"].ConnectionString;
        }

        private void LoadAllQuestions()
        {
            string connectionString = GetConnectionString();
            List<Question> allQuestions = new List<Question>();
            int pageNumber = 1; // Start with page 1

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Loop to load questions until all are fetched
                while (true)
                {
                    SqlCommand cmd = new SqlCommand("dbo.GetQuestionsWithPagination", conn); // Use the correct stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", 10);  // Adjust page size if needed

                    SqlDataReader reader = cmd.ExecuteReader();

                    // If no more questions, break the loop
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        break;
                    }

                    while (reader.Read())
                    {
                        var question = new Question
                        {
                            QuestionID = Convert.ToInt32(reader["QuestionID"]),
                            QuestionText = reader["QuestionText"].ToString(),
                            LastUpdated = Convert.ToDateTime(reader["LastUpdated"]),
                            Choices = LoadChoices(Convert.ToInt32(reader["QuestionID"])) // Load choices for each question
                        };
                        allQuestions.Add(question);
                    }

                    reader.Close();
                    pageNumber++;  // Move to the next page
                }
            }

            questionsCache = allQuestions;  // Cache all questions in memory
        }


        private List<Choice> LoadChoices(int questionID)
        {
            string connectionString = GetConnectionString();
            List<Choice> choices = new List<Choice>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetChoices", conn); // Use stored procedure to get choices
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuestionID", questionID);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var choice = new Choice
                    {
                        ChoiceID = Convert.ToInt32(reader["ChoiceID"]),
                        ChoiceText = reader["ChoiceText"].ToString(),
                        IsCorrect = Convert.ToBoolean(reader["IsCorrect"])
                    };
                    choices.Add(choice);
                }
                reader.Close();
            }

            return choices;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(username.Text))  
            {
                if (!examStarted)  
                {
                    LoadAllQuestions();  
                    StartExam();  
                    button1.Text = "ส่งคำตอบ";  
                }
                else 
                {
                    StopExam();  
                    button1.Text = "เริ่มต้นสอบ";  
                }
            }
            else
            {
                MessageBox.Show("โปรดระบุชื่อ-นามสกุล ก่อนเริ่มสอบ", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void StartExam()
        {
            examStarted = true;
            timeElapsed = 0;
            lblTimer.Text = "เวลาที่ใช้ไป: 00:00";
            timer.Start();
            button1.Enabled = true;

            currentQuestionIndex = 0;  
            LoadCurrentQuestion();  
        }

        private void StopExam()
        {
            timer.Stop();
            CalculateScore();
            MessageBox.Show($"คุณใช้เวลา: {FormatTime(timeElapsed)}");
            button1.Enabled = true;
            examStarted = false;
        }

        private void CalculateScore()
        {
            int score = 0;

            foreach (var question in questionsCache)
            {
                if (question.Choices.Any(c => c.IsCorrect && c.IsSelected))
                {
                    score++;
                }
            }

            SaveResultToDatabase(username.Text, score, timeElapsed);

            //MessageBox.Show($"คุณ {username.Text} ได้คะแนน: {score}/{questionsCache.Count}");
            label2.Text = $"คุณ {username.Text} ได้คะแนน: {score}/{questionsCache.Count}";

        }


        private void SaveResultToDatabase(string username, int score, int timeElapsed)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SaveExamResult", conn); 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Score", score);
                cmd.Parameters.AddWithValue("@TimeSpent", timeElapsed);

                cmd.ExecuteNonQuery(); // Execute the stored procedure
            }
        }

        private void LoadCurrentQuestion()
        {
            if (questionsCache.Count == 0)
            {
                MessageBox.Show("ไม่พบคำถาม", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentQuestionIndex >= 0 && currentQuestionIndex < questionsCache.Count)
            {
                var currentQuestion = questionsCache[currentQuestionIndex];  // Get the current question

                DisplayQuiz(currentQuestion);
            }
            else
            {
                MessageBox.Show("คำถามไม่ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayQuiz(Question question)
        {
            panel1.Controls.Clear();

            if (question != null)
            {
                QuizChoiceControl choiceControl = new QuizChoiceControl
                {
                    QuestionText = question.QuestionText,
                    Choice1Text = question.Choices[0].ChoiceText,
                    Choice2Text = question.Choices[1].ChoiceText,
                    Choice3Text = question.Choices[2].ChoiceText,
                    Choice4Text = question.Choices[3].ChoiceText
                };

                choiceControl.Dock = DockStyle.Fill;

                int selectedIndex = question.Choices.FindIndex(c => c.IsSelected);
                choiceControl.SetSelectedChoice(selectedIndex);

                panel1.Controls.Add(choiceControl);
            }
        }

        private void SaveSelectedChoice()
        {
            if (panel1.Controls.Count > 0 && panel1.Controls[0] is QuizChoiceControl choiceControl)
            {
                int selectedIndex = choiceControl.SelectedChoiceIndex();

                questionsCache[currentQuestionIndex].Choices.ForEach(c => c.IsSelected = false);

                if (selectedIndex >= 0)
                {
                    questionsCache[currentQuestionIndex].Choices[selectedIndex].IsSelected = true;
                }
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            SaveSelectedChoice();
            if (currentQuestionIndex < questionsCache.Count - 1)
            {
                currentQuestionIndex++;
                LoadCurrentQuestion();
            }
            else
            {
                MessageBox.Show("คุณมาถึงข้อสุดท้ายแล้ว");
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            SaveSelectedChoice(); 
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                LoadCurrentQuestion();
            }
            else
            {
                MessageBox.Show("คุณอยู่ที่คำถามแรก");
            }
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            lblTimer.Text = $"เวลาที่ใช้ไป: {FormatTime(timeElapsed)}";
        }

        private string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            return $"{minutes:D2}:{remainingSeconds:D2}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Test the database connection when the form loads
            TestDatabaseConnection();
        }
    }
}
