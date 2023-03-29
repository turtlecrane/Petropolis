using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    //어떤 대사가 들어가는지 저장하는 스크립트
    //key와 value가 들어감
    private Dictionary<int, string[]> talkData;
    private Dictionary<int, int> StatusData;
    
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();//초기화
        StatusData = new Dictionary<int, int>();
        GenerateData();
    }

    void GenerateData()
    {
        //1000 = Remy, 2000 = Doctor, 3000 = Test
        // 구분자 목록 >> :0 = 대기, :1 = 긍정, :2 = 부정
        talkData.Add(1000, new string[]{ "안녕?:0" , "넌 작고 귀엽구나 !:0" , "그래 !:1" , "아니 !:2"});
        talkData.Add(2000, new string[]{ "걱정마렴 넌 너무나 건강해:0" , "주위를 둘러보는건 어떠니?:0", "그래 !:1" , "아니 !:2"});
        talkData.Add(3000, new string[]{ "npc테스트용 박스입니다:0","그래 !:1" , "아니 !:2"});
    }

    //한 문장씩 이 함수가 가져와서 리턴해주는 방식
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null; //대화 문장이 끝이나면 null을 날린다
        }
        else
        {
            return talkData[id][talkIndex];//끝이 아니면 뒤에 있는 대사 날리기
        }
        
    }
    
}
