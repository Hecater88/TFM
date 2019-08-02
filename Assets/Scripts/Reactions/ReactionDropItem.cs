using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDropItem : Reaction {

	// nombre dle objeto a eliminar del inventario
	public string itemName;


	/// <summary>
	/// Corrutina que será ejecutada como reacción.
	/// </summary>
	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);

		// eliminamos el objeto del inventario
		InventoryManager.IM.RemoveItemInventory (itemName);

	}
}
