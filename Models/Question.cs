using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp_No10.Models
{
    public class Question
    {
        public int QuestionID { get; set; } // ไอดีของคำถาม
        public string QuestionText { get; set; } // ข้อความของคำถาม
        public DateTime LastUpdated { get; set; } // เวลาที่คำถามได้รับการอัปเดต
        public List<Choice> Choices { get; set; } // ตัวเลือกคำตอบของคำถามนี้
    }

}

