using UnityEngine;

public class AnswerData : MonoBehaviour
{
    public const int AnswerData_Range = 51;

    [HideInInspector] public string[] answer_data = new string[AnswerData_Range] { "나서지 말자", "뒤돌아 보지 마", "아끼다 똥 된다", "오늘 말고 내일 하자!", "마음에 두지마라", "주변 사람들에게 조금 의지해 보는 게 어때?", "시간이 약이다", "세상에 공짜는 없잖아", "마음을 열고 기다려봐", "급할수록 돌아가라", "너도 네 마음을 알고 있잖아", "완벽할 필요 없어, 지금도 충분해", "지금이 아니면 안돼", "반성은 해도 되는데 후회는 하지 마", "기다리면 좋은 일이 생길거야", "새로운 것이 기다리고 있어", "이 또한 지나가리라", "멀리서 찾지 말고 가까운 데서 찾아봐", "한 발 뒤로 물러나 지켜보자", "다른 가능성도 열어둬", "준비된 자가 기회를 잡는다", "멈춰", "한계를 두지 마", "다른 사람의 말에 귀 기울여봐", "자신감을 가져도 좋아", "끝날 때까지 끝난 게 아니야", "모두 너의 바람대로 이루어질거야", "기회를 잡아", "마음이 가는 대로 해", "할 수 있어! 가보자고!", "정신 차려", "지금 당장 시작해봐", "잘 될거야, 늘 그랬던 것처럼", "결정한 대로 밀어붙여", "다음 기회를 노려보자", "벌어지지 않은 일에 너무 큰 걱정 하지마", "잠시 휴식이 필요해", "기회는 네가 만드는 거야", "안 하고 후회하는 것보다 하고 후회하는 게 낫지", "차근차근 생각하면 답이 나올거야", "울어도 돼", "그게 제일 큰 고민이야? 아니잖아, 다시 뽑아보자", "늦었다고 생각할 때가 진짜 늦은거야", "포기하는 것도 방법이야", "나는 알아, 네가 얼마나 수고했는지", "인정할 건 인정하자", "즐길 수 없다면 피해라", "어차피 너 말고 아무도 못해", "지금 생각하는 그 사람에게 연락해", "이게 끝이 아니야", "잘 안되고 일단 해보자" };

    int[] random_stack = new int[AnswerData_Range] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int previous_num = -1;//중복된 숫자 나오지 않도록 이전 숫자 저장, 처음에는 이전 숫자가 없으므로 절대 나오지 않을 -1로 정의해놓기

    public string rand_answer()//랜덤 대답 반환
    {
        int rand = BestRandomRange(0, AnswerData_Range);
        return answer_data[rand];
    }

    public int BestRandomRange(int min, int max)//최대한 골고루 나오도록
    {
        WHILE:
        int rand = Random.Range(min, max+1);//랜덤숫자고르기
        while (true)//최선의 숫자인지 반복하며 검증
        {
            if (rand == previous_num)//이 전 숫자와 겹치는지 확인
            {
                goto WHILE;//겹친다면 그냥 아예 처음으로 되돌려버리기
            }
            int max_stack = random_stack[0];//최댓값을 저장하는 변수
            int max_index = 0;//최댓값이 들어있는 인덱스 번호 저장하는 변수
            int count_bottom = 0;//가장 낮은 값(0)이 몇개 있는지 저장하는 변수
            for (int i = 1; i < max; i++)//반복하면서 최댓값 찾기
            {
                if (max_stack == 0)//가장 낮은 값(0)이면 개수 세기
                {
                    count_bottom++;
                }
                if (max_stack < random_stack[i])//확인하는 수가 더 클 때
                {
                    max_stack = random_stack[i];//해당 수 저장
                    max_index = i;//해당 수가 있는 인덱스 번호 저장
                }
            }
            if (count_bottom == 0)//가장 낮은 값(0)이 없으면 1씩 빼서 크기 줄이기
            {
                for (int i = 0; i < max; i++)
                {
                    random_stack[i]--;
                }
            }
            if (rand == max_index && max_stack != 0)//뽑은 rand값이 이미 너무 많이 나왔다면(최댓값이라면) 다시 고르기, && 처음 골랐을 때 맨 처음 숫자를 무조건 안고르지 않도록 조건 걸어주기
            {
                rand = Random.Range(min, max + 1);
            }
            else
            {
                random_stack[max_index]++;//지금 나온 숫자 카운트하기
                break;//반복문 종료
            }
        }

        previous_num = rand;//다음 값을 위해 지금 나온 값 저장하기
        return rand;//최적의 랜덤값을 반환
    }
}