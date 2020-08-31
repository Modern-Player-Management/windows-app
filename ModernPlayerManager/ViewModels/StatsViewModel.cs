using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
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
        private List<PlayersStat> playersStats;

        public string PlayerName1 => this.playersStats?[0].Player ?? "";
        public string PlayerName2 => this.playersStats?[1].Player ?? "";
        public string PlayerName3 => this.playersStats?[2].Player ?? "";

        private async void GetStats()
        {

            var values = await this.TeamApi.GetStats(this.teamId);
            playersStats = values;
            OnPropertyChanged(nameof(PlayerName1));
            OnPropertyChanged(nameof(PlayerName2));
            OnPropertyChanged(nameof(PlayerName3));
            this.scoreChartView.Chart = new BarChart()
            {
                BackgroundColor = SKColors.Transparent,
                MinValue = 0,
                MaxValue = 2000,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                Entries = values.Select(stats => new ChartEntry(stats.Score)
                        {Label = stats.Player, ValueLabel = stats.Score.ToString(), Color = SKColor.Parse("#ff4169")})
                    .ToList()
            };

            this.shootingChartView.Chart = new BarChart()
            {
                BackgroundColor = SKColors.Transparent,
                MinValue = 0,
                MaxValue = 100,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                Entries = values.Select(stats => new ChartEntry(stats.GoalShots)
                {
                    Label = stats.Player, ValueLabel = stats.GoalShots.ToString(), Color = SKColor.Parse("#296497")
                })
            };

            LoadPlayerStats(values[0], this.player1ChartView);
            LoadPlayerStats(values[1], this.player2ChartView);
            LoadPlayerStats(values[2], this.player3ChartView);
        }

        private static void LoadPlayerStats(PlayersStat playersStat, ChartView chartView)
        {
            chartView.Chart = new BarChart()
            {
                BackgroundColor = SKColors.Transparent,
                MinValue = 0,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                Entries = new List<ChartEntry>
                {
                    new ChartEntry(playersStat.Goals)
                        {Label = "Goals", ValueLabel = playersStat.Goals.ToString(), Color = SKColor.Parse("#ff6384")},
                    new ChartEntry(playersStat.Shots)
                        {Label = "Shots", ValueLabel = playersStat.Shots.ToString(), Color = SKColor.Parse("#36a2eb")},
                    new ChartEntry(playersStat.Assists)
                        {Label = "Assists", ValueLabel = playersStat.Assists.ToString(), Color = SKColor.Parse("#ffce56")},
                    new ChartEntry(playersStat.Saves)
                        {Label = "Saves", ValueLabel = playersStat.Saves.ToString(), Color = SKColor.Parse("#00cd22")}
                }
            };
        }


        public ITeamApi TeamApi = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

        private readonly string teamId;
        private readonly ChartView scoreChartView;
        private readonly ChartView shootingChartView;
        private readonly ChartView player1ChartView;
        private readonly ChartView player2ChartView;
        private readonly ChartView player3ChartView;

        public StatsViewModel(string teamId, ChartView scoreChartView, ChartView shootingChartView,
            ChartView player1ChartView, ChartView player2ChartView, ChartView player3ChartView)
        {
            this.teamId = teamId;
            this.scoreChartView = scoreChartView;
            this.shootingChartView = shootingChartView;
            this.player1ChartView = player1ChartView;
            this.player2ChartView = player2ChartView;
            this.player3ChartView = player3ChartView;

            GetStats();
        }
    }
}