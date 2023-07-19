using System;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010D RID: 269
public class StorySheepStatusDisplay : MonoBehaviour
{
	// Token: 0x06000796 RID: 1942
	private void Awake()
	{
		this.rootRectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000797 RID: 1943
	private void OnEnable()
	{
		this.AnimateOnScreenComplete();
	}

	// Token: 0x06000798 RID: 1944
	private void AnimateOffScreen()
	{
		this.currentDisplayState = StorySheepStatusDisplay.DisplayState.AnimatingOut;
		this.movementTween = Tween.AnchoredPosition(this.rootRectTransform, new Vector3(0f, 280f, 0f), 0.5f, 0f, Tween.EaseIn, Tween.LoopType.None, null, new Action(this.AnimateOffScreenComplete), true);
	}

	// Token: 0x06000799 RID: 1945
	private void AnimateOffScreenComplete()
	{
		this.currentDisplayState = StorySheepStatusDisplay.DisplayState.OffScreen;
	}

	// Token: 0x0600079A RID: 1946
	private void AnimateOnScreen()
	{
		this.currentDisplayState = StorySheepStatusDisplay.DisplayState.AnimatingIn;
		if (this.movementTween != null)
		{
			this.movementTween.Stop();
		}
		this.movementTween = Tween.AnchoredPosition(this.rootRectTransform, new Vector3(0f, 0f, 0f), 0.5f, 0f, Tween.EaseOut, Tween.LoopType.None, null, new Action(this.AnimateOnScreenComplete), true);
	}

	// Token: 0x0600079B RID: 1947
	private void AnimateOnScreenComplete()
	{
		this.currentDisplayState = StorySheepStatusDisplay.DisplayState.OnScreen;
		this.rootRectTransform.anchoredPosition = Vector3.zero;
		this.onScreenTimer = 2f;
	}

	// Token: 0x0600079C RID: 1948
	private void Update()
	{
		if (this.onScreenTimer > 0f)
		{
			this.onScreenTimer -= Time.deltaTime;
		}
	}

	// Token: 0x0600079D RID: 1949
	public void UpdateSheepPlayerIndexes(int shaunPlayerIndex, int timmyPlayerIndex, int shirleyPlayerIndex)
	{
		this.sheepPlayerIndex[0] = shaunPlayerIndex;
		this.sheepPlayerIndex[1] = timmyPlayerIndex;
		this.sheepPlayerIndex[2] = shirleyPlayerIndex;
		this.UpdateSheepDisplay(0, shaunPlayerIndex);
		this.UpdateSheepDisplay(1, timmyPlayerIndex);
		this.UpdateSheepDisplay(2, shirleyPlayerIndex);
		if (this.currentDisplayState == StorySheepStatusDisplay.DisplayState.OffScreen || this.currentDisplayState == StorySheepStatusDisplay.DisplayState.AnimatingOut)
		{
			this.AnimateOnScreen();
			return;
		}
		this.onScreenTimer = 2f;
	}

	// Token: 0x0600079E RID: 1950
	private void UpdateSheepDisplay(int sheepIndex, int playerIndex)
	{
		Color colourForPlayerIndex = this.GetColourForPlayerIndex(playerIndex);
		Sprite numberSpriteForPlayerIndex = this.GetNumberSpriteForPlayerIndex(playerIndex);
		Image image = this.colouredTabImages[sheepIndex];
		this.colouredMarkerImages[sheepIndex].color = colourForPlayerIndex;
		image.color = colourForPlayerIndex;
		Image image2 = this.numberImages[sheepIndex];
		if (numberSpriteForPlayerIndex != null)
		{
			image2.sprite = numberSpriteForPlayerIndex;
			image2.SetNativeSize();
			image2.gameObject.SetActive(true);
			image.gameObject.SetActive(true);
			return;
		}
		image2.gameObject.SetActive(false);
		image.gameObject.SetActive(false);
	}

	// Token: 0x0600079F RID: 1951
	private Color GetColourForPlayerIndex(int index)
	{
		switch (index)
		{
		case 0:
			return this.playerOneColour;
		case 1:
			return this.playerTwoColour;
		case 2:
			return this.playerThreeColour;
		default:
			return this.noPlayerColour;
		}
	}

	// Token: 0x060007A0 RID: 1952
	private Sprite GetNumberSpriteForPlayerIndex(int index)
	{
		switch (index)
		{
		case 0:
			return this.numberOneSprite;
		case 1:
			return this.numberTwoSprite;
		case 2:
			return this.numberThreeSprite;
		default:
			return null;
		}
	}

	// Token: 0x0400079E RID: 1950
	public Color playerOneColour;

	// Token: 0x0400079F RID: 1951
	public Color playerTwoColour;

	// Token: 0x040007A0 RID: 1952
	public Color playerThreeColour;

	// Token: 0x040007A1 RID: 1953
	public Color noPlayerColour;

	// Token: 0x040007A2 RID: 1954
	public Image[] colouredMarkerImages;

	// Token: 0x040007A3 RID: 1955
	public Image[] colouredTabImages;

	// Token: 0x040007A4 RID: 1956
	public Image[] numberImages;

	// Token: 0x040007A5 RID: 1957
	public Sprite numberOneSprite;

	// Token: 0x040007A6 RID: 1958
	public Sprite numberTwoSprite;

	// Token: 0x040007A7 RID: 1959
	public Sprite numberThreeSprite;

	// Token: 0x040007A8 RID: 1960
	private StorySheepStatusDisplay.DisplayState currentDisplayState;

	// Token: 0x040007A9 RID: 1961
	private int[] sheepPlayerIndex = new int[]
	{
		0,
		-1,
		-1
	};

	// Token: 0x040007AA RID: 1962
	private RectTransform rootRectTransform;

	// Token: 0x040007AB RID: 1963
	private TweenBase movementTween;

	// Token: 0x040007AC RID: 1964
	private float onScreenTimer;

	// Token: 0x040007AD RID: 1965
	private const float onScreenDuration = 2f;

	// Token: 0x040007AE RID: 1966
	private const float movementTweenDuration = 0.5f;

	// Token: 0x040007AF RID: 1967
	private const float onScreenPosY = 0f;

	// Token: 0x040007B0 RID: 1968
	private const float offScreenPosY = 280f;

	// Token: 0x040010DE RID: 4318
	private bool isToggled = true;

	// Token: 0x020001F6 RID: 502
	private enum DisplayState
	{
		// Token: 0x04000E06 RID: 3590
		OnScreen,
		// Token: 0x04000E07 RID: 3591
		AnimatingOut,
		// Token: 0x04000E08 RID: 3592
		OffScreen,
		// Token: 0x04000E09 RID: 3593
		AnimatingIn
	}
}
