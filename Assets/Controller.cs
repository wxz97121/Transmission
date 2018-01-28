using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Sprite[] Pic;
    public Person[] PersoninOrder;
    public string[] Title;
    Dictionary<string, string[]> Dict=new Dictionary<string, string[]>();
    private AudioSource m_Audio;
    private AudioClip WinAudio;
    private AudioClip FailAudio;
    private Text WinText;
    [SerializeField]
    private string WinString = "0231";
    [HideInInspector]
    public bool isChecking = false;
    private void Awake()
    {
        WinText = GameObject.Find("WinText").GetComponent<Text>();
        WinAudio = Resources.Load<AudioClip>("Win");
        FailAudio = Resources.Load<AudioClip>("Fail");
        m_Audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        Dict["0231"] = new string[] { "我已经忘了自己童年想要的玩具是什么了。但是任天堂还记得。", "哭了，真哭了。真丢人。我打过了一战二战，打过恐怖分子，打过外星人，打过诸神诸魔。我掌握过世界文明，掌控过人类历史。我见过最绚烂的景色，也经历过最惊悚的时刻。你让我发现了，其实我还是那个坐在地板上摆弄变形金刚最嘴里还自带配音的小男孩。虽然我还没有NS。", "任天堂Labo这个创意真他妈牛逼疯了！80刀造机器人也太酷了吧！希望不要变成下一个Kinect……", "唔，任天堂又发新产品了，感觉挺有趣的。" };
        Dict["0213"] = new string[] { "我已经忘了自己童年想要的玩具是什么了。但是任天堂还记得。", "哭了，真哭了。真丢人。我打过了一战二战，打过恐怖分子，打过外星人，打过诸神诸魔。我掌握过世界文明，掌控过人类历史。我见过最绚烂的景色，也经历过最惊悚的时刻。你让我发现了，其实我……", "安静！你话太多啦！吵到我玩天下第一的猛汉瓦鲁多了！", "傻逼索尼！怪猎世界居然是独占中文？" };
        Dict["1230"] = new string[] { "任天堂将于4月20日发布……4月20日……4月20日……这日子……卧槽！战神将于4月20日发售！", "妈耶，这作战神也太不战神了吧！没混沌之刃的奎爷还是奎爷吗？没摇杆小♡游♡戏的战神还是战神吗？严重差评。虽然没有PS4，但还是希望圣莫妮卡也考虑一下我这种视频通关了系列所有作的老玩家感受。", "主机独占是世界的恶意！Play Anywhere！", "猎天使魔女秒了。" };
        Dict["1203"] = new string[] { "任天堂将于4月20日发布……4月20日……4月20日……这日子……卧槽！战神将于4月20日发售！", "妈耶，这作战神也太不战神了吧！没混沌之刃的奎爷还是奎爷吗？没摇杆小♡游♡戏的战神还是战神吗？严重差评。虽然没有PS4，但还是希望圣莫妮卡也考虑一下我这种视频通关了系列所有作的老玩家感受。", "猎天使魔女秒了。", "主机独占是世界的恶意！Play Anywhere！" };
        Dict["3201"] = new string[] { "任天堂还没吸取到教训吗？又出这么个玩新鲜的东西，铁定跟Kinect一样吃灰，大写的服。", "一堆破纸板箱本来看着就烦人了，现在你跟我说80刀？？？图纸还不开源？我又卜是傻逼！有这80刀，我为什么不去买其他外设？我为什么不去拼乐高机器人？我为什么要买个容易坏的烂纸盒？看不懂见个纸盒钢琴就高潮的任豚。", "都说了80刀里带了游戏本体了，你在其他平台买个游戏花个60刀不是很正常吗。实在不喜欢纸盒你可以买数字版嘛，没有人逼着你买……哎，心疼没有童心的人。", "怎么又吵起来了，好烦。" };
        Dict["3210"] = new string[] { "任天堂还没吸取到教训吗？又出这么个玩新鲜的东西，铁定跟Kinect一样吃灰，大写的服。", "一堆破纸板箱本来看着就烦人了，现在你跟我说80刀？？？图纸还不开源？我又卜是傻逼！有这80刀，我为什么不去买其他外设？我为什么不去拼乐高机器人？我为什么要买个容易坏的烂纸盒？看不懂见个纸盒钢琴就高潮的任豚。", "唔，任天堂又发新产品了，感觉挺有趣的。", "我已经忘了自己童年想要的玩具是什么了。但是任天堂还记得。" };
    }
    public void Stop()
    {
        DOTween.Clear();
        StopAllCoroutines();
        foreach (Person p in PersoninOrder) p.Rewind();
    }
    public void Check()
    {
        Stop();
        isChecking = true;
        StartCoroutine(IEnumCheck());
    }
    public IEnumerator IEnumCheck()
    {
        string ans = "";
        foreach (Person p in PersoninOrder) ans += p.ID.ToString();
        //print(ans);
        string[] tmp = Dict[ans];
        if (tmp != null)
        {
            //print(tmp.Length);
            for (int i = 0; i < 4; i++)
            {
                StartCoroutine(PersoninOrder[i].Show(tmp[i]));
                while (!PersoninOrder[i].Done) yield return new WaitForFixedUpdate();
            }
        }
        if (ans == WinString)
        {
            PlayAudio(WinAudio);
            WinText.DOFade(1, 1);
        }
        else PlayAudio(FailAudio);
    }
    void PlayAudio(AudioClip Clip)
    {
        m_Audio.Stop();
        m_Audio.clip = Clip;
        m_Audio.Play();
    }
}
