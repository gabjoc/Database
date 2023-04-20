﻿namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Controller for working with 'Preke' entity.
/// </summary>
public class PrekeController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	//naudojam list nes tai reikia matyti
	public ActionResult Index()
	{
		return View(PrekeRepo.ListPreke());
	}
	
	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	//naudoja CE nes CE yra pritaikytas create ir edit mode, t.y. naudojami skaiciukai
	//su populate selections konvertuojame skaiciuka i pavadinima
	public ActionResult Create()
	{
		var preke = new PrekeCE();
		PopulateSelections(preke);
		return View(preke);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="preke">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(PrekeCE prek)
	{
		
		//form field validation passed?
		if( ModelState.IsValid )
		{
			PrekeRepo.Insert(prek);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}
		
		//form field validation failed, go back to the form
		PopulateSelections(prek);
		return View(prek);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var preke = PrekeRepo.FindCE(id);
		PopulateSelections(preke);
		return View(preke);
	}
	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="prek">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, PrekeCE prek)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			PrekeRepo.Update(prek);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}
		//form field validation failed, go back to the form
		PopulateSelections(prek);
		return View(prek);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var preke = PrekeRepo.FindL(id);
		return View(preke);
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
			PrekeRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var preke = PrekeRepo.FindL(id);
			return View("Delete", preke);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="prek">'PrekeCE' view model to append to.</param>
	//skirtas tam kad parduotuvesid galetume matyti kaip jos pavadinima
	public void PopulateSelections(PrekeCE prek)
	{
		//load entities for the select lists
		var kategorijos = KategorijaRepo.ListKategorija();
		var gamintojai = GamintojasRepo.ListGamintojas();

		//build select lists
		prek.Lists.Kategorijos = 
			kategorijos.Select(it => {
				return
					new SelectListItem() { 
						Value = Convert.ToString(it.Id), 
						Text = it.Pavadinimas
					};
			})
			.ToList();

		//build select lists
		prek.Lists.Gamintojai = 
			gamintojai.Select(it => {
				return
					new SelectListItem() { 
						Value = Convert.ToString(it.Id), 
						Text = it.Pavadinimas + " - " + it.Salis
					};
			})
			.ToList();
	}
}
