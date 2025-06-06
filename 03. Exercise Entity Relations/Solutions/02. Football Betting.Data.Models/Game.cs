﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Game
{
    [Key]
    public int GameId { get; set; }

    [Required]
    [ForeignKey(nameof(HomeTeam))]
    public int HomeTeamId { get; set; }

    public Team HomeTeam { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(AwayTeam))]
    public int AwayTeamId { get; set; }

    public Team AwayTeam { get; set; } = null!;

    [Required]
    public int HomeTeamGoals { get; set; }

    [Required]
    public int AwayTeamGoals { get; set; }

    [Required]
    public decimal HomeTeamBetRate { get; set; }

    [Required]
    public decimal AwayTeamBetRate { get; set; }

    [Required]
    public decimal DrawBetRate { get; set; }

    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    public string Result { get; set; } = null!;

    public ICollection<PlayerStatistic> PlayersStatistics { get; set; } = new HashSet<PlayerStatistic>();

    public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
}