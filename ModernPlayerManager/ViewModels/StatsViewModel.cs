using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microcharts.Uwp;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using Refit;
using SkiaSharp;

namespace ModernPlayerManager.ViewModels
{
    public class StatsViewModel : NotificationBase
    {
        private Chart scoreChart;

        public Chart ScoreChart
        {
            get => scoreChart;
            private set
            {
                scoreChart = value;
                OnPropertyChanged();
                foreach (var scoreChartEntry in this.ScoreChart.Entries) {
                    Debug.WriteLine(scoreChartEntry.Label);
                    Debug.WriteLine(scoreChartEntry.Value.ToString());
                }
                
            }
        }

        private async void GetStats() {
            var values = await this.TeamApi.GetStats(this.teamId);
            this.scoreChartView.Chart = new BarChart()
            {
                MinValue = 0,
                MaxValue = 2000,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                Entries = values.Select(stats => new ChartEntry(stats.Score)
                    { Label = stats.Player,ValueLabel = stats.Score.ToString(), Color = SKColor.Parse("#ff4169") }).ToList()
            };

            this.shootingChartView.Chart = new BarChart()
            {
                MinValue = 0,
                MaxValue = 100,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                Entries = values.Select(stats => new ChartEntry(stats.GoalShots)
                {
                    Label = stats.Player, ValueLabel = stats.GoalShots.ToString(), Color = SKColor.Parse("#296497")
                })
            };
            
        }


        public ITeamApi TeamApi = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

        private readonly string teamId;
        private readonly ChartView scoreChartView;
        private readonly ChartView shootingChartView;

        public StatsViewModel(string teamId, ChartView scoreChartView, ChartView shootingChartView) {
            this.teamId = teamId;
            this.scoreChartView = scoreChartView;
            this.shootingChartView = shootingChartView;

            GetStats();
        }
    }
}