using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Enemy resultEnemyData;
    // 적 데이터베이스 클래스
    public class EnemyDatabase
    {
        // 적 목록
        public static List<Enemy> enemies = new List<Enemy>();

        // 적을 추가하는 함수
        public static void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        // 적을 가져오는 함수
        public static Enemy GetEnemy(int enemyId)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.EnemyID == enemyId)
                {
                    return enemy;
                }
            }

            return null;
        }
    }

    // 적 클래스
    public class Enemy
    {
        // 적 고유번호
        public int EnemyID { get; set; }

        // 적 이름
        public string[] EnemyName { get; set; }

        // 적 유형
        public int EnemyType { get; set; }
        /* 적 유형 번호
          1: 일반
          2: 보스
          3: 특수
         */

        // 적 AI 모델 유형
        public int EnemyAI { get; set; }
        /*AI 모델 번호
          0: "Nothing" 아무것도 안함
          1: "Dumb" 무작위 행동
          2: "Agressive" 공격을 할 확률이 높음
          3: "Defensive" 방어행동을 할 확률이 높음
          4: "Shy" 가능하면 도주 행동을 할 확률이 높음
          5: "Smart" 공격적 행동을 하다 일정 HP이하로 떨어지면 방어적인 행동으로 바꾸며, 매번 가장 효과적인 공격을 한다.
          6: "Agressive Extreme" 공격적 행동을 할 확률이 높으며, 매번 가장 효과적인 공격을 한다.

        (그 외의 번호는 고유 AI 모델이다.)
        */

        // 최대 HP 값
        public int MaxHP { get; set; }

        // 기본 AP 값
        public int BaseAP { get; set; }

        // 공격력
        public int Attack { get; set; }

        // 방어력
        public int Defense { get; set; }

        // 특수 공격력
        public int SpecialAttack { get; set; }

        // 특수 방어력
        public int SpecialDefense { get; set; }

        // 속도
        public int Speed { get; set; }

        // 행운
        public int Luck { get; set; }




        //행동 팔레트

        // 공격 행동 팔레트
        public int[] AtkPalette { get; set; }

        // 방어 행동 팔레트
        public int[] DefPalette { get; set; }

        // 버프 행동 팔레트
        public int[] BuffPalette { get; set; }

        // 디버프 행동 팔레트
        public int[] DebuffPalette { get; set; }

        // 상태 이상 스킬 팔레트
        public int[] StatusEffectPalette { get; set; }

        // 기타 스킬 팔레트
        public int[] EtcSkillPalette { get; set; }



        //플레이어 보상

        //보상 경험치
        public int RewardExp;

        //보상 돈
        public int RewardMoney;

        //드랍 아이템
        public int[] RewardItems { get; set; }

        //아이템 드랍 확률
        public float ItemDropPossibility { get; set; }

        //아이템 드랍 비율
        public int[] DropRatio { get; set; }

        // TODO: 적 스프라이트 데이터 구현

        // 생성자
        public Enemy(int enemyId, string[] enemyName, int enemyType, int enemyAI, 
            int maxHP, int baseAP, int attack, int defense, int spAtk, int spDef, int speed, int luck, 
            int[] atkPal, int[] defPal, int[] buffPal, int[] debuffPal, int[] effectPal, int[] etcPal,
            int rewardExp, int rewardMoney, int[] rewardItems, float itemDropPossibility, int[] dropRatio)
        {
            this.EnemyID = enemyId;
            this.EnemyName = enemyName;
            this.EnemyType = enemyType;
            this.EnemyAI = enemyAI;

            this.MaxHP = maxHP;
            this.BaseAP = baseAP;
            this.Attack = attack;
            this.Defense = defense;
            this.SpecialAttack = spAtk;
            this.SpecialDefense = spDef;
            this.Speed = speed;
            this.Luck = luck;

            this.AtkPalette = atkPal;
            this.DefPalette = defPal;
            this.BuffPalette = buffPal;
            this.DebuffPalette = debuffPal;
            this.StatusEffectPalette = effectPal;
            this.EtcSkillPalette = etcPal;

            this.RewardExp = rewardExp;
            this.RewardMoney = rewardMoney;
            this.RewardItems = rewardItems;
            this.ItemDropPossibility = itemDropPossibility;
            this.DropRatio = dropRatio;
        }
    }

    void Start()
    {
        //파일에서 적 이미지 추출
        LoadImages();


        // **예시1** //
        EnemyDatabase.AddEnemy(new Enemy(
            1,      //적 ID
            new string[] { "더미", "Dummie" },     //적 이름
            1,      //적 유형
            1,      //적 AI
            100,    //최대 HP
            100,    //베이스 AP
            20,     //공격력
            20,     //방어력
            20,     //특공
            20,     //특방
            100,    //스피드
            100,    //행운
            new int[] { 1 },  //공격 행동 팔레트
            new int[] { 2, 3 },//방어 행동 팔레트
            new int[] { },  //버프 행동 팔레트
            new int[] { 10 }, //디버프 행동 팔레트
            new int[] { 11 }, //상태이상 행동 팔레트
            new int[] { },  //기타 행동 팔레트
            30,             //보상 경험치
            100,            //보상 돈
            new int[] { 2, 3 },//보상 아이템
            0.33f,          //아이템 자체의 드랍 가능성
            new int[] { 10, 1 }//아이템 드랍 비율
            ));


        // **예시2** //
        EnemyDatabase.AddEnemy(new Enemy(
            2,      //적 ID
            new string[] { "구름 아조씨", "Mr.Thunder Cloud" },     //적 이름
            1,      //적 유형
            1,      //적 AI
            100,    //최대 HP
            100,    //베이스 AP
            20,     //공격력
            20,     //방어력
            20,     //특공
            20,     //특방
            100,    //스피드
            100,    //행운
            new int[] { 1 },  //공격 행동 팔레트
            new int[] { 2, 3 },//방어 행동 팔레트
            new int[] { },  //버프 행동 팔레트
            new int[] { 10 }, //디버프 행동 팔레트
            new int[] { 11 }, //상태이상 행동 팔레트
            new int[] { },  //기타 행동 팔레트
            30,             //보상 경험치
            100,            //보상 돈
            new int[] { 2, 3 },//보상 아이템
            0.33f,          //아이템 자체의 드랍 가능성
            new int[] { 10, 1 }//아이템 드랍 비율
            ));

    }


    //적 이미지
    public string relativeFolderPath; // 상대 경로로 폴더 경로 지정
    public Sprite[] imageArray; // 이미지를 저장할 배열

    void LoadImages()
    {
        string folderPath = Path.Combine(Application.dataPath, relativeFolderPath); // 상대 경로를 절대 경로로 변환

        string[] files = Directory.GetFiles(folderPath, "*.png"); // 폴더 내의 모든 PNG 파일 가져오기

        imageArray = new Sprite[(files.Length + 1) * 10]; // 이미지 배열 초기화

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file); // 파일 이름 (확장자 제외)

            // 파일 이름에서 숫자 추출
            string[] splitName = fileName.Split('-');
            if (splitName.Length > 1)
            {
                int index;
                if (int.TryParse(splitName[0], out index)) // 숫자로 변환
                {
                    // 이미지 로드
                    Texture2D texture = LoadTextureFromFile(file);
                    if (texture != null)
                    {
                        // Texture2D를 Sprite로 변환하면서 FilterMode를 None으로 설정
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f, 100, 0, SpriteMeshType.FullRect, new Vector4(0, 0, texture.width, texture.height), false);
                        sprite.texture.filterMode = FilterMode.Point; // FilterMode를 None으로 설정

                        // 이미지 배열에 할당
                        imageArray[index] = sprite;
                    }
                }
            }
        }
    }

    Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // 파일 데이터로부터 텍스처 로드
        return texture;
    }


    //원격 적 검색
    public void SetSearchEnemy(int enemyID)
    {
        resultEnemyData = EnemyDatabase.GetEnemy(enemyID);
    }
}
