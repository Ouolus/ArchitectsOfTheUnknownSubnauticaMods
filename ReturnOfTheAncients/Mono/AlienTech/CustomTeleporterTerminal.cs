using UnityEngine;
using Story;

namespace RotA.Mono.AlienTech
{
    public class CustomTeleporterTerminal : HandTarget, IHandTarget
    {
        public TechType[] acceptedTechTypes;
        public string storyGoalName;
        public string interactText;

		public PlayerCinematicController cinematicController;

		public Animator animator;

		public FMODAsset useSound;

		public FMODAsset openSound;

		public FMODAsset closeSound;

		public GameObject root;

		private GameObject itemObject;

		private int restoreQuickSlot = -1;

		public bool Unlocked { get { return StoryGoalManager.main.IsGoalComplete(storyGoalName); } }

		private void Start()
		{
			isValidHandTarget = false;
		}

		public void OpenDeck()
		{
			if (Unlocked)
			{
				return;
			}
			animator.SetBool("Open", true);
			Utils.PlayFMODAsset(openSound, transform, 20f);
		}

		public void CloseDeck()
		{
			if (animator.GetBool("Open"))
			{
				animator.SetBool("Open", false);
				Utils.PlayFMODAsset(closeSound, transform, 20f);
			}
		}

		public void OnHandClick(GUIHand hand)
        {
			if (!Unlocked)
			{
				Pickupable pickupable = Inventory.main.container.RemoveItem(TechType.PrecursorIonCrystal);
				if (pickupable != null)
				{
					restoreQuickSlot = Inventory.main.quickSlots.activeSlot;
					Inventory.main.ReturnHeld(true);
					itemObject = pickupable.gameObject;
					itemObject.transform.SetParent(Inventory.main.toolSocket);
					itemObject.transform.localPosition = Vector3.zero;
					itemObject.transform.localRotation = Quaternion.identity;
					itemObject.SetActive(true);
					Rigidbody component = itemObject.GetComponent<Rigidbody>();
					if (component != null)
					{
						component.isKinematic = true;
					}
					cinematicController.StartCinematicMode(Player.main);
					Utils.PlayFMODAsset(useSound, transform, 20f);
					StoryGoalManager.main.OnGoalComplete(storyGoalName);
				}
			}
		}

        public void OnHandHover(GUIHand hand)
        {
            if (!Unlocked)
            {
                HandReticle.main.SetInteractText(interactText, false);
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }
        }

		public void OnPlayerCinematicModeEnd(PlayerCinematicController controller)
		{
			if (itemObject)
			{
				Destroy(itemObject);
			}
			if (root)
			{
				root.BroadcastMessage("ToggleDoor", true, SendMessageOptions.RequireReceiver);
			}
			else
			{
				BroadcastMessage("ToggleDoor", true, SendMessageOptions.RequireReceiver);
			}
			CloseDeck();
			if (restoreQuickSlot != -1)
			{
				Inventory.main.quickSlots.Select(restoreQuickSlot);
			}
		}
	}
}
