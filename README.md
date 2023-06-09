# Petropolis
[2023 한성대학교 캡스톤 디자인] </br></br>
**반려동물과 인간의 공존을 위한 교육형 어드벤처 게임 콘텐츠**
</br></br>
## 프로젝트 배경

<details><summary>프로젝트 배경
</summary>
</br>
통계청의 ‘2020 인구주택총조사’에 따르면 전체 가구 중 15%인 312만 9천 가구가 반려동물을 키우는 것으로 집계되었다. </br>
반려동물과 생활하는 인구가 늘어남에 따라 반려동물 관련 서비스, 가전제품 등의 시장 역시 커지는 추세이다. </br>
이와 더불어 과거와 달리 동물권에 대한 관심이 늘어나고, 반려동물에 대한 정보 및 교육 콘텐츠에 대한 수요도 증가하고 있다. </br></br>
이번 프로젝트는 이러한 수요를 고려하여 기획하였고, 게임 콘텐츠를 진행하면서 자연스럽게 반려동물을 이해하고, 공존하기 위한 정보를 습득할 수 있는 콘텐츠를 제작하고자 하였다.
</details>
</br>

## 프로젝트 목표

<details><summary>프로젝트 목표
</summary>
</br>
- 이번 프로젝트는 위와 같은 수요를 고려하며 기획한, 친근한 이미지의 반려동물 고양이를 주인공으로 하며, 가상 공간인 마을을 돌아다니면서 다양한 사건을 해결하는 어드벤처 게임이다. </br></br>
- 플레이어는 주어진 가상 공간을 돌아다니며 다양한 오브젝트, NPC, 미니게임 등의 콘텐츠를 경험할 수 있다. </br></br>
- 이러한 콘텐츠들을 통해 플레이어는 반려동물과 생활하면서 겪을 수 있는 다양한 상황을 직간접적으로 경험하게 된다. 이를 해결하는 과정을 통해 플레이어가 자연스럽게 반려동물을 이해하고 공존하기 위한 정보를 습득하게 하는 것을 목표로 한다.

</details>
</br></br>


## 프로젝트 구조

![structure](/readme/structure.png)

</br>

1. Unity : 기반 게임 엔진
2. Raider Editor : C# 스크립트 편집 소프트웨어
3. Blender : 3D 모델링 편집 소프트웨어
4. Git : 협업용 분산형 버전 관리 시스템

</br></br>

## 프로젝트 결과물
</br>

**1. MainMap 전경**

<img src="/readme/MainMap.png" width="500">


<details><summary>설명
</summary>

>Opening-Scene과 Ending-Scene을 제외한 게임의 주요 콘텐츠를 즐길 수 있는 가상공간으로, 도시라는 콘셉트에 맞추어 주택가, 공원, 상점가, 쓰레기장의 지역으로 나뉘어 있다. 플레이어는 해당 가상공간을 돌아다니면서 다양한 오브젝트, NPC 등과 상호작용하며 게임 콘텐츠를 즐긴다.
</details>

</br>

---

</br>





**2. 상호작용**

<img src="/readme/Interact_1.png" width="500"> <img src="/readme/Interact_2.png" width="500">
</br>
<img src="/readme/Interact_3.png" width="500"> <img src="/readme/Interact_4.png" width="500">

<details><summary>설명
</summary>

>가상공간 속 오브젝트, NPC 등의 가까이 접근하여 클릭하여 각종 상호작용을 할 수 있다. 플레이어는 여러 상호작용을 통해 반려동물 활동에 대한 정보를 직간접적으로 습득할 수 있다. </br>예를 들어 섭취하는 음식물 오브젝트의 종류에 따라 플레이어 캐릭터의 상태를 조정하여 어떤 음식을 급식할 수 있을지 간접적으로 알 수 있고, NPC와의 대화를 통해 다양한 정보를 얻는 식으로 구성되어 있다.
</details>
</br>

---

</br>



**3. 퀘스트 상호작용**

<img src="/readme/quest_1.png" width="500"> <img src="/readme/quest_2.png" width="500"> 
</br>

<details><summary>설명
</summary>

>느낌표 UI가 나타나 있는 NPC와 상호작용하여 여러 퀘스트를 수주할 수 있다. 퀘스트의 내용은 반려동물 생활과 관련된 것들로 이루어져 있다. </br>플레이어는 퀘스트를 해결하면서 자연스럽게 해당 정보를 습득할 수 있다. 플레이어 활동에 대한 정보는 플레이 중 저장되어 Ending-Scene에서 플레이어의 등급을 정하는 데 활용된다.
</details>
</br>

---

</br>



**4. 상태 시스템**

<img src="/readme/condition_1.png" width="500">
<img src="/readme/condition_2.png" width="500">
<img src="/readme/condition_3.png" width="500">


<details><summary>설명
</summary>

>고양이에게 좋지 못한 음식을 섭취하는 등의 행동이 플레이어 캐릭터에게 부정적인 영향을 주고 이는 상태 UI에 반영된다. </br>이를 방치하면 차차 건강 상태가 악화하고 종국에는 게임오버 되거나 수의사 NPC와 상호작용하여 상태를 호전시킬 수 있다. </br>이를 통해 플레이어로 하여금 반려동물의 음식에 대한 정보를 간접적으로 습득하도록 한다.
</details>
</br>

---

</br>


**5. 미니게임**

<img src="/readme/minigame_1.png" width="500"> <img src="/readme/minigame_2.png" width="500">
</br>
<img src="/readme/minigame_3.png" width="500"> <img src="/readme/minigame_4.png" width="500">

<details><summary>설명
</summary>

>콘텐츠 도중 할 수 있는 미니게임을 여러 종류 삽입하였다. 주가 되는 콘텐츠와 확연하게 구별되는 미니게임들로 패러디를 활용하는 등 플레이어의 주위를 환기하여 꾸준히 흥미를 유발한다. </br>또한 미니게임의 내용 역시 반려동물 생활과 연관된 것으로 구성하여 이러한 정보를 제공한다.
</details>
</br>

---

</br>



**6. Cut - Scene**

<img src="/readme/openingCut_1.png" width="500"> <img src="/readme/openingCut_2.png" width="500">
</br>

<details><summary>설명
</summary>

>Opening-Scene과 Ending-Scene에 사용된 Cut-Scene이다. 간단한 스토리를 보여주거나, 플레이어 캐릭터의 이름을 지어주는 등 플레이어의 몰입을 돕기 위해 삽입되었다.
</details>
</br>

---

</br>



**7. 튜토리얼**

<img src="/readme/tuto_1.png" width="500"> <img src="/readme/tuto_2.png" width="500"> 
</br>

<details><summary>설명
</summary>

>플레이어에게 본 게임 콘텐츠의 조작법을 알려주는 튜토리얼이다. 직접 조작하면서 조작법을 익힐 수 있고, 콘텐츠를 즐기는 중간에 특정 Key를 입력하면 UI가 나타나 이미지와 텍스트로 이루어진 튜토리얼을 제공하여 플레이어의 진행을 돕는다.
</details>
</br>

---

</br>



**8. Ending-Scene**

<img src="/readme/Ending_1.png" width="500"> 
<img src="/readme/Ending_2.png" width="500">
<img src="/readme/Ending_3.png" width="500"> 


<details><summary>설명
</summary>

>콘텐츠를 진행하고 나서 짧은 Cut-Scene과 함께 Ending-Scene으로 넘어간다. </br>여기에서는 게임 중 저장된 플레이어의 행동에 대한 데이터를 바탕으로 점수를 매겨 표시한다. 이를 통해 플레이어가 그동안의 활동을 되돌아볼 수 있도록 유도하였다.
</details>
</br>

---

</br>



**9. 소리설정**

<img src="/readme/sound.png" width="500"> 
</br>

<details><summary>설명
</summary>

> 게임 시작 전과 게임 중 BGM 및 효과음의 음량을 조절할 수 있는 Slide-bar를 제공한다.
</details>
</br>

---

</br>
</br></br>



   
## 역할 분담
| 멤버 | 역할 | 담당 분야 |
| --- | --- | --- |
| @turtlecrane | 팀장 | 기획, 에셋 제작, 기반 시스템 구현, UI 구현 |
| @NEWBBWEN | 팀원 | 기획, 개발 계획 구상, 에셋 제작, 내부 콘텐츠 제작 |
| @kr-user | 팀원 | 에셋 제작, 기반 시스템 구현, 내부 콘텐츠 제작 |
| @khs1871074 | 팀원 | 에셋 제작, 기반 시스템 구현, 내부 콘텐츠 제작 |

<br>  <br> 


