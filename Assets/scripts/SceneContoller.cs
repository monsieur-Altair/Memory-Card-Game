using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContoller : MonoBehaviour
{
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] imageArray;
    public const int numberOfRows = 2;
    public const int numberOfColumns = 4;
    public const float deltaX = 4f;
    public const float deltaY = 5f;
    private MemoryCard firstRevealedCard;
    private MemoryCard secondRevealedCard;
    private int score = 0;
    [SerializeField] TextMesh scoreText;
    [SerializeField] private AudioClip scoreUp;
    [SerializeField] private AudioClip victory;
    [SerializeField] private Reload button;

    public int Score
    {
        get { return score; }
    }

    public bool CanReveal
    {
        get { return !secondRevealedCard; }
    }
    public void CardRevealed(MemoryCard card)
    {
        if (!firstRevealedCard)
            firstRevealedCard = card;
        else
        {
            secondRevealedCard = card;
            StartCoroutine(CheckReveal());
        }
    }

    private IEnumerator CheckReveal()
    {
        if (firstRevealedCard.id == secondRevealedCard.id)
        {
            yield return new WaitForSeconds(0.75f);
            scoreText.text = "score " + ++score;
            GetComponent<AudioSource>().PlayOneShot(scoreUp);
            firstRevealedCard.DestroyCard();
            secondRevealedCard.DestroyCard();
            if (score == 4)
            {
                button.TurnOnAnimation(true);
                GetComponent<AudioSource>().PlayOneShot(victory);
            }
               
        }
        else
        {
            yield return new WaitForSeconds(1.80f);
            firstRevealedCard.Unreveal();
            secondRevealedCard.Unreveal();
        }
        firstRevealedCard = null;
        secondRevealedCard = null;
    }
    void Start()
    {
        int[] idArray = { 0, 0, 1, 1, 2, 2, 3, 3 };
        idArray = ShuffleArray(idArray);

        Vector3 startPosition = originalCard.transform.position;
        for (int i = 0; i < numberOfRows; i++)
            for (int j = 0; j < numberOfColumns; j++)
            {
                MemoryCard card;
                if ((i == 0) && (j == 0))
                    card = originalCard;
                else
                    card = Instantiate(originalCard) as MemoryCard;
                
                int id = idArray[i * numberOfColumns + j];
                card.SetCard(id, imageArray[id]);

                float posX = startPosition.x + j * deltaX;
                float posY = startPosition.y - i * deltaY;
                card.transform.position = new Vector3(posX, posY, startPosition.z);
            }
    }

    public int[] ShuffleArray(int[] originalArray)
    {
        int tmp, randomIndex;
        int[] newArray = originalArray.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            randomIndex = Random.Range(i, newArray.Length);
            tmp = newArray[i];
            newArray[i] = newArray[randomIndex];
            newArray[randomIndex] = tmp;
        }
        return newArray;
    }
}
