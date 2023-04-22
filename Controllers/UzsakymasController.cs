namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Uzsakymas;


/// <summary>
/// Controller for working with 'Automobilis' entity.
/// </summary>
public class UzsakymasController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(UzsakymasRepo.ListUzsakymas());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var uzsak = new UzsakymasCE();
		PopulateSelections(uzsak);

		return View(uzsak);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="autoCE">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(UzsakymasCE uzsakCE)
	{
		//form field validation passed?
		if( ModelState.IsValid )
		{
			UzsakymasRepo.InsertUzsakymas(uzsakCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}
		
		//form field validation failed, go back to the form
		PopulateSelections(uzsakCE);
		return View(uzsakCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var uzsakCE = UzsakymasRepo.FindUzsakymasCE(id);
		PopulateSelections(uzsakCE);

		return View(uzsakCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="autoCE">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, UzsakymasCE uzsakCE)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			UzsakymasRepo.UpadateUzsakymas(uzsakCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(uzsakCE);
		return View(uzsakCE);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var uzsakCE = UzsakymasRepo.FindUzsakymasL(id);
		return View(uzsakCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try 
		{
			UzsakymasRepo.DeleteUzsakymas(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var uzsakCE = UzsakymasRepo.FindUzsakymasL(id);

			return View("Delete", uzsakCE);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="autoCE">'Automobilis' view model to append to.</param>
	public void PopulateSelections(UzsakymasCE uzsakCE)
	{
		//load entities for the select lists
		var pristatymai = UzsakymasRepo.ListPristatymas();
		var busenos = UzsakymasRepo.ListUzsakymoBusena();
		var klientai = KlientasRepo.List();
		var darbuotojai = DarbuotojasRepo.ListDarbuotojas();

		//build select lists
		uzsakCE.Lists.Klientai = 
			klientai.Select(it => {
				return
					new SelectListItem() { 
						Value = Convert.ToString(it.PirkejasNr), 
						Text = it.Vardas + " " + it.Pavarde 
					};
			})
			.ToList();

		uzsakCE.Lists.Darbuotojai = 
			darbuotojai.Select(it => {
				return
					new SelectListItem() { 
						Value = Convert.ToString(it.Tabelis), 
						Text = it.Vardas + " " + it.Pavarde
					};
			})
			.ToList();

			uzsakCE.Lists.Pristatymai = 
			pristatymai.Select(it => {
				return
					new SelectListItem() { 
						Value = Convert.ToString(it.Id), 
						Text = it.Pavadinimas
					};
			})
			.ToList();

			uzsakCE.Lists.Busenos = 
			busenos.Select(it => {
				return
					new SelectListItem() { 
						Value = Convert.ToString(it.Id), 
						Text = it.Pavadinimas
					};
			})
			.ToList();
	}
}
