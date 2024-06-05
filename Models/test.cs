using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestClassTest.Models {
    public class TestClass {
        [Key]
        public int TestID { get; set; } // Primary Key
        public string TestName { get; set; } = string.Empty;
    }
}