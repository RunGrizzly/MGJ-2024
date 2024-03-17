using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _leaderBoardCanvasGroup;

    [SerializeField]
    private MedalWidget _medalWidgetTemplate;
    private MedalWidget _medalWidgetInstance = null;


    [SerializeField] private LeaderboardLabel _labelTemplate;
    [SerializeField] private CinemachineTargetGroup _leaderboardFramer = null;
    [SerializeField] private Transform floorBound = null;
    [SerializeField] private Transform ceilingBound = null;



    public SerializableDictionary<MedalType, Player> medalMap = new SerializableDictionary<MedalType, Player>();


    private void OnEnable()
    {
        Brain.ins.EventHandler.EndRoundEvent.AddListener(SyncLeaderboard);
        Brain.ins.EventHandler.MedalEarnedEvent.AddListener(DisplayMedal);
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(OnBlockSettled);
        Brain.ins.EventHandler.EndRoundEvent.AddListener(OnRoundEnd);


    }

    private void OnDisable()
    {
        Brain.ins.EventHandler.EndRoundEvent.RemoveListener(SyncLeaderboard);
        Brain.ins.EventHandler.MedalEarnedEvent.RemoveListener(DisplayMedal);
        Brain.ins.EventHandler.BlockSettledEvent.RemoveListener(OnBlockSettled);
        Brain.ins.EventHandler.EndRoundEvent.RemoveListener(OnRoundEnd);

        if (_medalWidgetInstance != null)
        {
            LeanTween.cancel(gameObject);
            Destroy(_medalWidgetInstance.gameObject);
            _medalWidgetInstance = null;
        }
    }

    private void OnRoundEnd(Round round)
    {
        if (round.State == RoundState.Pass)
        {
            LeaderboardLabel newLabel = Instantiate(_labelTemplate, transform);

            var labelPos = round.Blocks[round.Blocks.Count - 1].GetHighestPoint();
            labelPos.z = -1;
            labelPos.x = 0;

            newLabel.transform.position = labelPos;

            newLabel.NameBox.text = round.Player.Name;
        }

    }

    private void OnBlockSettled(Block block)
    {
        _leaderboardFramer.m_Targets[0].target = floorBound;
        _leaderboardFramer.m_Targets[1].target = ceilingBound;

        ceilingBound.position = block.transform.position;
    }

    private void DisplayMedal(Player player, MedalType medalType)
    {
        SpawnWidget(medalType);

        if (!medalMap.Contains(medalType))
        {
            medalMap.Add(medalType, player);
        }
        else
        {
            medalMap[medalType] = player;
        }

        // Debug.LogFormat("{0} achieved {1}", player.Name, medalType.ToString());
    }

    private void SyncLeaderboard(Round context)
    {
        Debug.LogFormat("Leaderboard received a sync call from an round with the {0} win condition", context.ToString());
    }


    private void SpawnWidget(MedalType medalType)
    {
        if (_medalWidgetInstance != null)
        {
            LeanTween.cancel(gameObject);
            Destroy(_medalWidgetInstance.gameObject);
            _medalWidgetInstance = null;
        }

        _medalWidgetInstance = Instantiate(_medalWidgetTemplate, _leaderBoardCanvasGroup.transform);
        _medalWidgetInstance.WidgetText.text = medalType.ToString();

        if (!Brain.ins.AudioManager.IsBusy())
        {
            Brain.ins.EventHandler.PlaySFXEvent.Invoke(_medalWidgetInstance.MedalSpawnSting, 0);
            Brain.ins.EventHandler.PlaySFXEvent.Invoke(_medalWidgetInstance.MedalJingleSting, 0);
            Brain.ins.EventHandler.PlaySFXEvent.Invoke(_medalWidgetInstance.MedalWhooshSting, 0.5f);
        }

        LeanTween.value(gameObject, 0, 1, 0.5f).setEase(LeanTweenType.easeOutElastic)

        .setOnUpdate((float val) =>
        {
            _medalWidgetInstance.transform.localScale = Vector3.one * val;
        })

        .setOnComplete(() =>
        {
            LeanTween.delayedCall(gameObject, 0.85f, () =>
            {
                LeanTween.value(0, Screen.width, 0.6f).setEase(LeanTweenType.easeOutExpo)


                .setOnUpdate((float val) =>
                {
                    _medalWidgetInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(-val, _medalWidgetInstance.GetComponent<RectTransform>().anchoredPosition.y);
                })

                .setOnComplete(() => Destroy(_medalWidgetInstance.gameObject));
            });
        });

    }

}
