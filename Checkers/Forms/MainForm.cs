using Checkers.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Checkers.Forms
{
    /// <summary>
    /// Основная форма приложения
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly IUserService _userService;
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(IUserService userService) : this()
        {
            _userService = userService;
        }
    }
}
