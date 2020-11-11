using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace VocabLearner.Controller
{
    public class Word
    {
        [PrimaryKey]
        public string sourceWord { get; set; }
        public string translatedWord { get; set; }
    }
}
