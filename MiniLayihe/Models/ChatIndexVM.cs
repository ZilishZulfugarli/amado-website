using System;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class ChatIndexVM
	{
		public AppUser? User { get; set; }
        public bool IsAdmin { get; internal set; }
    }
}

