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
        // 구분자 목록 >> :0 = 대기, :1 = 긍정, :2 = 부정, (->애니메이션 관련 추가 예정)
        //              :9 = ObjID를 1증가시켜서 대화 종료후 다시 상호작용 하면 다른 대사가 출력됨
        //              :10 = 플레이어 대사 (10으로 구분해두면 이름도 플레이어로 바뀌게 해놓음)
        //주인
        talkData.Add(1000, new string[]{ "안녕♬?:0" , "넌 작고 귀엽구나 !:0" , "그래 !:1" , "아니 !:2"});
        
        //의사
        talkData.Add(2000, new string[]{ "너는 저번에 분양된 아이 아니니?:0" , "왜 혼자 돌아다니고 있는걸까...:0", "무슨 일인지는 모르겠지만 우선 상태를 봐야겠다:98"});
            //상태이상 체크후 상태에 따라서 의사의 ID가 2001이 되거나 2002가 되게해야함.
        talkData.Add(2001, new string[]{ "상태가 좋구나! \n가족들이 걱정할텐데.. 어서 집으로 돌아가렴!:1"});
        talkData.Add(2002, new string[]{ "상태가 좋지 않구나.. 우선 치료부터 받으렴:99"});
        talkData.Add(2003, new string[]{ "이제 괜찮아질거야. :0", "가족들이 걱정할텐데.. 어서 집으로 돌아가렴!:9"});
        talkData.Add(2004, new string[]{ "아직도 집으로 가지 않은거야?:0"});
        
        //브레인
        talkData.Add(3000, new string[]{ "이번엔 다른 대사가 나올까..?:0","그래 !:1" , "아니 !:2", "다음에 말 걸면 대화가 바뀔거야.. :9"});
        talkData.Add(3001, new string[]{ "바뀐 대사야 어때? :0", "바뀐 대사에서도 이전 대사처럼 \n 자유롭게 대화가 가능해! 야호 :1"});
        
        //소피
        talkData.Add(4000, new string[]{ "나는 지정된 구역을 돌아다니고 있어. :0", "비켜줄래??????? :9"});
        talkData.Add(4001, new string[]{ "좀 비키라고 !!:1"});
        
        //제임스
        talkData.Add(5000, new string[]{ "음흠흠♬ 음흠♩..:0"});
        
        //조쉬
        talkData.Add(6000, new string[]{ "오늘은 뭘 살까나 - :0"});
        
        //케이트
        talkData.Add(7000, new string[]
        {
            "음? 여기에 왜 키우던 동물이 있는거지? \n 너.. 집을 잃은거야?:0",
            "아이고.. 얼른 집으로 돌아가! 주인이 걱정하겠네..:9"
        });
        talkData.Add(7001, new string[]{ "아직도 집으로 안간거야? :2"});
        
        //레오널드
        talkData.Add(8000, new string[]{ "오늘 날씨 좋다! \n 산책하기 딱 좋은 날씨인걸? :0"});
        
        //공원 미니게임 NPC
        talkData.Add(10000, new string[]{ "못보던 아이구나? :0", "음... 혹시 프리스비 게임을 같이 하고싶니? :0"});
        talkData.Add(10001, new string[]{ "좋아! 그럼 같이 해보자! :1"});
        talkData.Add(10002, new string[]{ "안하고 싶은가보네? 생각이 바뀌면 알려줘 :0"});
        
        //공원 홀로있는 동물NPC
        talkData.Add(11000,  new string[]{ "안녕? 공원에서 혼자 뭐하고 있니? :10",
            "음..가족이랑 함께 산책을 나왔는데 어느샌가 사라졌지 뭐야 :0", "혼자 떨어져 있으니까 너무 불안해... :0",
            "그럼 나도 함께 가족을 찾아줄게.:10", "그 사람 어떻게 생겼는지 알려줄래? :10",
            "정말? 고마워 그 사람은 빨간 옷에 어쩌구 저쩌구..:0", 
            "좋아. 우리 같이 한 번 찾아보자! :10",
            "응! 가자! :9"
        });
        talkData.Add(11001, new string[]{ "먼저 출발해~:0"});
            //주인을 찾으면 아이디가 증가(++)하게 구현되어야함.
        talkData.Add(11002, new string[]{ "와! 맞아 바로 이 사람이야! :0", "덕분에 바로 가족을 찾았어 고마워! :0", "앞으로 산책할 땐 잃어버리지 않게 조심해야겠다 :0"});
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
