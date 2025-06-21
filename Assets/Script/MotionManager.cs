using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionManager : MonoBehaviour
{
    public AnswerData AnswerData;
    public AudioManager AudioManager;

    public Animator Card_ani;
    public Text answer_text;

    public GameObject CardShffling_screen;
    public GameObject CardOpening_screen;

    const int dummy_count = 6;
    public GameObject[] CardDummy = new GameObject[dummy_count];
    bool[] dummy_shuffle_bool = new bool[dummy_count] { false, false, false, false, false, false };
    bool[] shuffleCoroutine_bool = new bool[dummy_count] { false, false, false, false, false, false };

    public bool OnButton = false;
    public bool Card_opened = false;//ī�带 �����ִ� �������� �ݰ��ִ� ��������
    public bool Card_animation = false;//ī�� �ִϸ��̼� ��������� �ƴ���

    public bool Shuffled = true;//ī�尡 �������� ����, �ȼ������� ���� ī�� �״�� �ٽ� �����ֱ�

    public void OpeningScreen(bool open)
    {
        CardOpening_screen.SetActive(open);
    }

    public void CardOpen()
    {
        if (Shuffled)
        {
            answer_text.text = AnswerData.rand_answer();
            Shuffled = false;
        }
        else if (!Shuffled)
        {
            //���� ��� �״�� �ٽ� �����ֱ�
        }

        if (!Card_animation)
        {
            if (!Card_opened)
            {
                Card_ani.SetBool("card_opened", true);
                AudioManager.open_audio();
                Card_opened = true;
            }
            else if (Card_opened)
            {
                Card_ani.SetBool("card_opened", false);
                AudioManager.open_audio();
                Card_opened = false;
            }
        }
    }


    public void StopShuffle()
    {
        StopCoroutine("StartShuffle");
        OnButton = false;
        dummy_shuffle_bool = new bool[] {false, false, false, false, false, false };
    }

    public void StartShuffle_Coroutine()
    {
        AudioManager.startShuffle_audio();
        CardShffling_screen.SetActive(true);
        Shuffled = true;
        StartCoroutine("StartShuffle");
    }
    public IEnumerator StartShuffle()
    {
        //print("StartShuffle");
        OnButton = true;
        for (int i = 0; i < dummy_count; i++)
        {
            dummy_shuffle_bool[i] = true;
            StartCoroutine("CardSpin", i);
            yield return new WaitForSeconds(0.07f);
        }
        yield break;
    }


    public IEnumerator CardSpin(int DummyNum)
    {
        shuffleCoroutine_bool[DummyNum] = true;
        //print("CardSpin");
        while (true)
        {
            float card_angle = 0;

            for (card_angle = 0; card_angle < 180; card_angle += Time.deltaTime * 430f)
            {
                yield return null;
                CardDummy[DummyNum].transform.rotation = Quaternion.Euler(0, 0, card_angle);
                //print(CardDummy[DummyNum].transform.rotation.z);
                //print("card_angle : " + card_angle);
            }

            yield return null;
            CardDummy[DummyNum].transform.rotation = Quaternion.Euler(0, 0, 180);

            if (dummy_shuffle_bool[DummyNum])
            {
                //��� �ݺ��ϱ�
            }
            else
            {
                shuffleCoroutine_bool[DummyNum] = false;
                if (shuffleCoroutine_bool[0] == false && shuffleCoroutine_bool[1] == false && shuffleCoroutine_bool[2] == false && shuffleCoroutine_bool[3] == false && shuffleCoroutine_bool[4] == false)
                {
                    AudioManager.stopShuffle_audio();
                    CardShffling_screen.SetActive(false);
                }
                
                yield break;
            }
        }
    }
}
