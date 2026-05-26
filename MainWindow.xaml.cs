using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Input;

namespace part
{
    public partial class MainWindow : Window
    {
        Random random = new Random();

        string userName = "";
        bool waitingForName = true;

        List<string> passwordTips = new List<string>()
        {
            "Use strong passwords with symbols and numbers ",
            "Avoid using personal information in passwords.",
            "Change important passwords regularly."
        };

        List<string> phishingTips = new List<string>()
        {
            "Avoid clicking suspicious email links ",
            "Always verify unknown senders before replying.",
            "Phishing scams often create panic or urgency."
        };

        public MainWindow()
        {
            InitializeComponent();

            btnSend.Click += BtnSend_Click;
            btnPassword.Click += BtnPassword_Click;
            btnPhishing.Click += BtnPhishing_Click;
            btnBrowsing.Click += BtnBrowsing_Click;
            btnHelp.Click += BtnHelp_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void AddMessage(string sender, string message, Brush color, bool isUser)
        {
            Border bubble = new Border
            {
                Background = color,
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12),
                Margin = new Thickness(5),
                MaxWidth = 400,
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            TextBlock text = new TextBlock
            {
                Text = sender + ": " + message,
                Foreground = Brushes.White,
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = text;

            ChatPanel.Children.Add(bubble);
            ChatScrollViewer.ScrollToEnd();
        }

        private string GetBotResponse(string input)
        {
            input = input.ToLower();
            // ASKING FOR USER NAME
            if (waitingForName)
            {
                userName = input;

                waitingForName = false;

                return "Nice to meet you, " + userName + "! How can I help you today? ";
            }

            // MEMORY FEATURE
            if (input.StartsWith("my name is"))
            {
                userName = input.Replace("my name is", "").Trim();

                return "Nice to meet you, " + userName ;
            }

            // SENTIMENT DETECTION
            if (input.Contains("sad") || input.Contains("stressed"))
            {
                return "I'm sorry you're feeling that way, Stay safe online and take breaks when needed.";
            }

            // KEYWORD RECOGNITION
            if (input.Contains("password"))
            {
                return passwordTips[random.Next(passwordTips.Count)];
            }

            if (input.Contains("phishing"))
            {
                return phishingTips[random.Next(phishingTips.Count)];
            }

            if (input.Contains("browsing"))
            {
                return "Always check if websites use HTTPS before entering personal information ";
            }

            if (input.Contains("hello") || input.Contains("hi"))
            {
                if (userName != "")
                {
                    return "Welcome back, " + userName ;
                }

                return "Hello! How can I help you today?";
            }

            return "I'm still learning, but I'm here to help keep you cyber safe ";
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtUserInput.Text;

            if (string.IsNullOrWhiteSpace(userInput))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            AddMessage("You", userInput, Brushes.DeepSkyBlue, true);

            AddMessage("LockWise AI", "Typing...", Brushes.Gray, false);

            await Task.Delay(1200);

            ChatPanel.Children.RemoveAt(ChatPanel.Children.Count - 1);

            string response = GetBotResponse(userInput);

            AddMessage("LockWise AI", response, Brushes.MediumPurple, false);

            txtUserInput.Clear();
        }

        private void BtnPassword_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                passwordTips[random.Next(passwordTips.Count)],
                Brushes.MediumPurple,
                false);
        }

        private void BtnPhishing_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                phishingTips[random.Next(phishingTips.Count)],
                Brushes.MediumPurple,
                false);
        }

        private void BtnBrowsing_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                "Safe browsing protects your information online ",
                Brushes.MediumPurple,
                false);
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                "You can ask about passwords, phishing, browsing, or tell me your name ",
                Brushes.MediumPurple,
                false);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ChatPanel.Children.Clear();

            AddMessage("LockWise AI",
                " Welcome to LockWise AI! Ask me about cybersecurity.",
                Brushes.MediumPurple,
                false);
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSend_Click(sender, e);
            }
        
    }
    }
}