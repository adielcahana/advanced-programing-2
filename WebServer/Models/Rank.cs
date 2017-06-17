using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServer.Models
{
    public class Rank
    {
        public string Id { get; set; }
        [Required]
        public string JoinDate { get; set; }
        [Required]
        public int GamesWon { get; set; }
        [Required]
        public int GamesLost { get; set; }
    }
}