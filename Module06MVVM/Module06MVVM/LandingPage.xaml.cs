using Module06MVVM.View;
using System;
using Xamarin.Forms;

namespace Module06MVVM
{
    public partial class LandingPage : TabbedPage
    {
        private int quizQuestionIndex = 0;
        private int quizScore = 0;
        private readonly string[] quizQuestions = new[]
        {
            "What is the most effective way to reduce waste?",
            "Which gas is most responsible for global warming?",
            "What can you do to save water at home?"
        };

        private readonly string[][] quizAnswers = new[]
        {
            new[] { "Reduce", "Reuse", "Recycle" },
            new[] { "Carbon Dioxide", "Methane", "Nitrous Oxide" },
            new[] { "Take shorter showers", "Leave taps running", "Water your lawn every day" }
        };

        private readonly string[] quizCorrectAnswers = new[]
        {
            "Reduce",
            "Carbon Dioxide",
            "Take shorter showers"
        };

        private double translationX, translationY;

        public LandingPage()
        {
            InitializeComponent();

            // Placeholder event handlers for recycling game drag and drop functionality
            var paperItemGestureRecognizer = new PanGestureRecognizer();
            paperItemGestureRecognizer.PanUpdated += OnPanUpdated;
            PaperItem.GestureRecognizers.Add(paperItemGestureRecognizer);

            var plasticItemGestureRecognizer = new PanGestureRecognizer();
            plasticItemGestureRecognizer.PanUpdated += OnPanUpdated;
            PlasticItem.GestureRecognizers.Add(plasticItemGestureRecognizer);

            var glassItemGestureRecognizer = new PanGestureRecognizer();
            glassItemGestureRecognizer.PanUpdated += OnPanUpdated;
            GlassItem.GestureRecognizers.Add(glassItemGestureRecognizer);

            // Initialize quiz score
            QuizScoreLabel.Text = $"Score: {quizScore}";
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowEmployeePage());
        }

        private void OnQuizAnswerClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button.Text == quizCorrectAnswers[quizQuestionIndex])
            {
                QuizResultLabel.Text = "Correct!";
                quizScore++;
            }
            else
            {
                QuizResultLabel.Text = "Try again!";
                quizScore = 0; // Reset score on incorrect answer
            }
            QuizResultLabel.IsVisible = true;
            QuizScoreLabel.Text = $"Score: {quizScore}";

            quizQuestionIndex = (quizQuestionIndex + 1) % quizQuestions.Length;
            UpdateQuizQuestion();
        }

        private void UpdateQuizQuestion()
        {
            QuestionLabel.Text = $"Question: {quizQuestions[quizQuestionIndex]}";
            AnswerButton1.Text = quizAnswers[quizQuestionIndex][0];
            AnswerButton2.Text = quizAnswers[quizQuestionIndex][1];
            AnswerButton3.Text = quizAnswers[quizQuestionIndex][2];
        }

        private int treeMoveCount = 0;

        private void OnGrowTreeClicked(object sender, EventArgs e)
        {
            // Increase the size of the tree image
            TreeImage.ScaleTo(TreeImage.Scale * 1.2, 250, Easing.CubicInOut);
            treeMoveCount++;
            TreeCountLabel.Text = treeMoveCount.ToString();
        }

        private void OnResetTreeClicked(object sender, EventArgs e)
        {
            // Reset the tree image size and move count
            TreeImage.ScaleTo(1, 250, Easing.CubicInOut);
            treeMoveCount = 0;
            TreeCountLabel.Text = treeMoveCount.ToString();
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var image = sender as Image;
            if (image == null) return;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    translationX = image.TranslationX;
                    translationY = image.TranslationY;
                    break;

                case GestureStatus.Running:
                    image.TranslationX = translationX + e.TotalX;
                    image.TranslationY = translationY + e.TotalY;
                    break;

                case GestureStatus.Completed:
                    // Check if the item is in the correct bin
                    if (IsOverlapping(image, PaperBin) && image == PaperItem)
                    {
                        DisplayAlert("Recycling Game", "Correct! Paper goes in the paper bin.", "OK");
                    }
                    else if (IsOverlapping(image, PlasticBin) && image == PlasticItem)
                    {
                        DisplayAlert("Recycling Game", "Correct! Plastic goes in the plastic bin.", "OK");
                    }
                    else if (IsOverlapping(image, GlassBin) && image == GlassItem)
                    {
                        DisplayAlert("Recycling Game", "Correct! Glass goes in the glass bin.", "OK");
                    }
                    else
                    {
                        DisplayAlert("Recycling Game", "Incorrect! Try again!", "OK");
                    }

                    image.TranslationX = 0;
                    image.TranslationY = 0;
                    break;
            }
        }

        private bool IsOverlapping(VisualElement view1, VisualElement view2)
        {
            var bounds1 = new Rectangle(view1.X + view1.TranslationX, view1.Y + view1.TranslationY, view1.Width, view1.Height);
            var bounds2 = new Rectangle(view2.X, view2.Y, view2.Width, view2.Height);
            return bounds1.IntersectsWith(bounds2);
        }

        private void OnResetRecyclingGameClicked(object sender, EventArgs e)
        {
            PaperItem.TranslationX = 0;
            PaperItem.TranslationY = 0;
            PlasticItem.TranslationX = 0;
            PlasticItem.TranslationY = 0;
            GlassItem.TranslationX = 0;
            GlassItem.TranslationY = 0;
        }
    }
}
