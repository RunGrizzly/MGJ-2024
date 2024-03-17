using System.Linq;
using Cinemachine;
using ScriptableObjects;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private CanvasGroup _leaderBoardCanvasGroup;

    [SerializeField] private MedalWidget _medalWidgetTemplate = null;
    private MedalWidget _medalWidgetInstance = null;

    [SerializeField] private MedalPanelElement _medalPanelElementTemplate = null;
    private MedalPanelElement _medalPanelElementInstance = null;


    [SerializeField] private LeaderboardLabel _labelTemplate;
    [SerializeField] private CinemachineTargetGroup _leaderboardFramer = null;
    [SerializeField] private Transform floorBound = null;
    [SerializeField] private Transform ceilingBound = null;
    [SerializeField] private GameHistory _gameHistory = null;


    public SerializableDictionary<MedalType, MedalPanelElement> _medalPanelElements = new();
    public RectTransform _medalPanelElementList = null;


    private void OnEnable()
    {
        Brain.ins.EventHandler.EndRoundEvent.AddListener(SyncLeaderboard);
        Brain.ins.EventHandler.MedalEarnedEvent.AddListener(OnMedalEarned);
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(OnBlockSettled);
        Brain.ins.EventHandler.SessionEndEvent.AddListener(OnSessionEnd);

        foreach (var (key, value) in _gameHistory.Medals.ToList())
        {
            OnMedalEarned(value.Player, key, value.Score);
        }
    }

    private void OnDisable()
    {
        Brain.ins.EventHandler.EndRoundEvent.RemoveListener(SyncLeaderboard);
        Brain.ins.EventHandler.MedalEarnedEvent.RemoveListener(OnMedalEarned);
        Brain.ins.EventHandler.BlockSettledEvent.RemoveListener(OnBlockSettled);
        Brain.ins.EventHandler.SessionEndEvent.AddListener(OnSessionEnd);


        if (_medalWidgetInstance != null)
        {
            LeanTween.cancel(gameObject);
            Destroy(_medalWidgetInstance.gameObject);
            _medalWidgetInstance = null;
        }
    }

    private void OnSessionEnd(Round round)
    {
        if (round.State == RoundState.Pass)
        {
            LeaderboardLabel newLabel = Instantiate(_labelTemplate, transform);

            var labelPos = round.Blocks[round.Blocks.Count - 1].GetHighestPoint();
            labelPos.z = -1;
            labelPos.x = 0;

            newLabel.transform.position = labelPos;

            newLabel.NameBox.text = round.Player.Name;
            newLabel.Round = round;
        }
    }

    private void OnBlockSettled(Block block)
    {
        if (_leaderboardFramer.m_Targets[0].target == null)
        {
            _leaderboardFramer.m_Targets[0].target = floorBound;
        }

        if (_leaderboardFramer.m_Targets[1].target == null)
        {
            _leaderboardFramer.m_Targets[1].target = ceilingBound;
        }


        var targetPos = block.transform.position;
        targetPos.x = 0;
        targetPos.z = 0;

        LeanTween.delayedCall(0.25f, () =>
        {
            LeanTween.move(ceilingBound.gameObject, targetPos, 0.35f).setEase(LeanTweenType.easeInExpo);
        });

    }

    private void OnMedalEarned(Player player, MedalType medalType, int score)
    {
        SpawnWidget(medalType);

        if (!_medalPanelElements.ContainsKey(medalType))
        {
            MedalPanelElement newMedalPanelElement = Instantiate(_medalPanelElementTemplate, _medalPanelElementList);
            newMedalPanelElement.NameBox.text = player.Name;
            newMedalPanelElement.MedalTitle.text = medalType.ToString();
            newMedalPanelElement.MedalScore.text = score.ToString();

            _medalPanelElements.Add(medalType, newMedalPanelElement);
        }
        else
        {
            _medalPanelElements[medalType].NameBox.text = player.Name;
            _medalPanelElements[medalType].MedalScore.text = score.ToString();
        }


        if (!_gameHistory.Medals.Contains(medalType))
        {
            _gameHistory.Medals.Add(medalType, new MedalData
            {
                MedalType = medalType,
                Player = player,
                Score = score,
            });
        }
        else
        {
            _gameHistory.Medals[medalType] = new MedalData
            {
                MedalType = medalType,
                Player = player,
                Score = score
            };
        }

        // Debug.LogFormat("{0} achieved {1}", player.Name, medalType.ToString());
    }

    private void SyncLeaderboard(Round context)
    {
        Debug.LogFormat("Leaderboard received a sync call from an round with the {0} win condition",
            context.ToString());
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
            .setOnUpdate((float val) => { _medalWidgetInstance.transform.localScale = Vector3.one * val; })
            .setOnComplete(() =>
            {
                LeanTween.delayedCall(gameObject, 0.85f, () =>
                {
                    LeanTween.value(0, Screen.width, 0.6f).setEase(LeanTweenType.easeOutExpo)
                        .setOnUpdate((float val) =>
                        {
                            _medalWidgetInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(-val,
                                _medalWidgetInstance.GetComponent<RectTransform>().anchoredPosition.y);
                        })
                        .setOnComplete(() => Destroy(_medalWidgetInstance.gameObject));
                });
            });
    }
}