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
        
        //브라이언
        talkData.Add(3000, new string[]{ "응? 너는 이 동네에 사는 녀석아니니? : 0", "심심해서 잠깐 놀러 나온 거구나? 하하! :0", "그래도 네 가족이 널 걱정할 수 있으니 금방 돌아가렴! :9"});
        talkData.Add(3001, new string[]{ "아직 안돌아갔구나? 조금만 놀고 들어가자~ :0"});
        
        //소피
        talkData.Add(4000, new string[]{ "어머! 안녕 야옹아~ 나는 소피라고해~ 네 이름은 뭐니~? :0", "혹시 배고프니? 간식이라도 줄까? :9"});
        talkData.Add(4001, new string[]{ "어머, 또 찾아왔네? 나한테 무슨 볼 일이 있으려나~? :0"});
        
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
        
        //공원 레오널드
        talkData.Add(8000, new string[]{ "오늘 날씨가 참 좋구나... 너도 산책 나왔니? \n 허허.. 즐거운 시간 보내려무나.. :0"});
        
        //공원 메건
        talkData.Add(10000, new string[]
        {
            "검둥아! 어디갔었니! :0", 
            "한참 찾았잖아..! 그래도 찾아서 정말 다행이야!:88", // NPC옆에 개가 나옴
            "너가 우리 검둥이를 도와줬구나?:0", "어머.. 친절한 고양이구나~ 호호! :0",
            "고마워~ 작은 친구!:0", "이제 집에 가자 검둥아~:0",
            ":87" // 대화가 끝나며 NPC + 개 사라짐
        });
        /*talkData.Add(10001, new string[]{ "좋아! 그럼 같이 해보자! :1"});
        talkData.Add(10002, new string[]{ "안하고 싶은가보네? 생각이 바뀌면 알려줘 :0"});*/
        
        //공원 홀로있는 동물NPC
        talkData.Add(11000,  new string[]{ "안녕? 공원에서 혼자 뭐하고 있니? :10",
            "응.. 가족이랑 함께 산책을 나왔는데 어느샌가 사라졌지 뭐야? :0", "혼자 떨어져 있으니까 너무 불안해... :0",
            "그렇구나.. 그러면 나도 함께 가족을 찾아줄게.:10", "그 사람 어떻게 생겼는지 알려줄래? :10",
            "정말? 고마워!! 그녀는.. 흰 옷에 파란색 청바지를 입었어. :0", 
            "좋아. 그러면 우리 같이 한 번 찾아보자! :10",
            "응! 가자! :9",
            "과연 어디에 있을까? 공원을 한 번 돌아보자 :89", // 개가 사라짐
        });
        //talkData.Add(11002, new string[]{ "와! 맞아 바로 이 사람이야! :0", "덕분에 바로 가족을 찾았어 고마워! :0", "앞으로 산책할 땐 잃어버리지 않게 조심해야겠다 :0"});

        
        //공원 조디
        talkData.Add(12000, new string[]{ "어머~ 너희는 누구니~? 길을 잃어버렸나~?:0", "음... 아무래도 찾는 사람은 아닌거 같아.. :10"});
        
        //공원 브라이스
        talkData.Add(11100, new string[]{ "뭐야? 이 조그만 녀석은, 저리가~ 훠이~! :0"});
        
        //공원 아담
        talkData.Add(11300, new string[]{ "헛둘헛둘.. 응..? 너도 같이 운동하고 싶니? :0"});
        
        //공원 에이미
        talkData.Add(11200, new string[]{ "뭐야? 나랑 놀고 싶은거야? \n 그래! 같이 놀자~! :0", "음... 아무래도 찾는 사람은 아닌거 같아.. :10"});

        //상점 조
        talkData.Add(11400, new string[]{ "후우.. 오늘 면접 무사히 합격할 수 있을까... :0", "응? 위로해주는거냐? 녀석, 그래~ 화이팅이다! :0"});
        
        //상점 엘리자베스
        talkData.Add(11500, new string[]{ "자~ 자~! 싱싱한 과일 많이 들어왔어요! :0", "한 번 구경오세.. 어머, 너도 과일 사러왔니? 후후.. :0"});
        
        //상점 페트
        talkData.Add(11600, new string[]{ "안녕, 조그만 친구? 여기 상점가에 무슨 볼 일이라도? :0"});
        


        //퀴즈 NPC
        talkData.Add(14000, new string[]{ " Quiz Test :101"});
        talkData.Add(14001, new string[]{ " Quiz Failed :2"});
        talkData.Add(14002, new string[]{ " 흥. 너에게 알려줄 것은 없어 :2"});
        talkData.Add(14003, new string[]{ " 쓰레기장의 주위를 잘 둘러보면 구멍이 있을거야. 장애물들을 잘 피해보라구! :1"});
        
        // 쓰레기장 퀘스트
        talkData.Add(13000, new string[]{ " Quest Test :102"});
        talkData.Add(13001, new string[]{ " Quest Ing :1"});
        talkData.Add(13002, new string[]{ " Quest Clear :104"});
        talkData.Add(13003, new string[]{ " Quest End :1"});
        
        talkData.Add(13010, new string[]{ " Quest NPC :103"});

        //주택가 타임어택 NPC
        talkData.Add(15000, new string[]{"안녕? 왜 혼자서 울고 있니? :10",
        "가족 중에 누군가 내 장난감을 실수로 쓰레기통에 버렸어 :0", "정말로 아끼는 장난감인데... :0", "곧 사람들이 와서 쓰레기를 가져가겠지... :0",
        "그럼 내가 지금 되찾아다줄까?  :10",
        "앗, 그래준다면 정말 고맙지! :0", "하지만 곧 사람들이 올테니까 빨리 가야 찾을 수 있을거야 :0",
        "이 앞에 있는 쓰레기통에 아마 있을거야! :111"
        });
        talkData.Add(15001, new string[]{ "시간이 얼마 없어...! :0"});

        talkData.Add(15002, new string[]{ "못찾았나보네... :0", "괜찮아 이미 버려진건 어쩔 수 없지... \n 대신 찾아봐줘서 고마워 :0"
        });
        talkData.Add(15003, new string[]{ "정말로 찾아줬구나! :112", "대신 찾아줘서 고마워! :0", "누가 다시 못버리게 해야겠다 :0"
        });
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
            return talkData[id][talkIndex]; //끝이 아니면 뒤에 있는 대사 날리기
        }
        
    }
    
}
