namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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
		[MaxLength(11)]
		public int? Id { get; set; }

		public int? FkPreke { get; set; }

		[DisplayName("Kiekis")]
		[MaxLength(11)]
		[Required]
		public int Kiekis { get; set; }
	
		[DisplayName("Parduotuvė")]
		[Required]
		public int FkParduotuve { get; set; }
		
	} 

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Parduotuves { get; set; }
	}
	
	/// <summary>
	/// Likutis.
	/// </summary>
	public PrekesLikutisM Likutis { get ; set; } = new PrekesLikutisM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}
