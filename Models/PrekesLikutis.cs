﻿namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// 'PrekesLikutis' in create and edit forms.
/// </summary>
public class PrekesLikutis
{
    /// <summary>
    /// Preke.
    /// </summary>
    public class PrekesLikutisM
	{
		[DisplayName("Likucio ID")]
		public int? Id { get; set; }

		public int InListId { get; set; }

		public int? FkPreke { get; set; }

		[DisplayName("Kiekis")]
		[Required]
		public int Kiekis { get; set; }
	
		[DisplayName("Parduotuvė")]
		[Required]
		public int FkParduotuve { get; set; }
		
	} 
	
	/// <summary>
	/// Likutis.
	/// </summary>
	public PrekesLikutisM Likutis { get ; set; } = new PrekesLikutisM();
}
