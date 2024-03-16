using Unity.Mathematics;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _leaderBoardCanvasGroup;

    [SerializeField]
    private MedalWidget _medalWidgetTemplate;
    private MedalWidget _medalWidgetInstance = null;

    public SerializableDictionary<MedalType, Player> medalMap = new SerializableDictionary<MedalType, Player>();


    private void OnEnable()
    {
        Brain.ins.EventHandler.EndRoundEvent.AddListener(SyncLeaderboard);
        Brain.ins.EventHandler.MedalEarnedEvent.AddListener(DisplayMedal);
    }

    private void OnDisable()
    {
        Brain.ins.EventHandler.EndRoundEvent.RemoveListener(SyncLeaderboard);
        Brain.ins.EventHandler.MedalEarnedEvent.RemoveListener(DisplayMedal);

        if (_medalWidgetInstance != null)
        {
            LeanTween.cancel(gameObject);
            Destroy(_medalWidgetInstance.gameObject);
            _medalWidgetInstance = null;
        }
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
