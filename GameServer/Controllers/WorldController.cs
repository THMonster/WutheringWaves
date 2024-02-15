using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Event;
using Google.Protobuf.WellKnownTypes;
using Protocol;

namespace GameServer.Controllers;
internal class WorldController : Controller
{
    public WorldController(PlayerSession session) : base(session)
    {
        // WorldController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(CreatureController creatureController)
    {
        await creatureController.JoinScene(8);
        for (int i = 1; i < 14; i++)
        {
            await Session.Push(MessageId.MapUnlockFieldNotify, new MapUnlockFieldNotify { FieldId = i });
        }
    }

    [NetEvent(MessageId.EntityOnLandedRequest)]
    public ResponseMessage OnEntityOnLandedRequest() => Response(MessageId.EntityOnLandedResponse, new EntityOnLandedResponse());

    [NetEvent(MessageId.PlayerMotionRequest)]
    public ResponseMessage OnPlayerMotionRequest() => Response(MessageId.PlayerMotionResponse, new PlayerMotionResponse());

    [NetEvent(MessageId.EntityLoadCompleteRequest)]
    public ResponseMessage OnEntityLoadCompleteRequest() => Response(MessageId.EntityLoadCompleteResponse, new EntityLoadCompleteResponse());

    [NetEvent(MessageId.UpdateSceneDateRequest)]
    public ResponseMessage OnUpdateSceneDateRequest() => Response(MessageId.UpdateSceneDateResponse, new UpdateSceneDateResponse());

    [NetEvent(MessageId.MapUnlockFieldInfoRequest)]
    public async Task<ResponseMessage> OnMapUnlockFieldInfoRequest(MapUnlockFieldInfoRequest request, EventSystem eventSystem)
    {
        List<int> fieldId = new List<int>();

        for (int i = 1; i < 10; i++)
        {
            await Session.Push(MessageId.MapUnlockFieldNotify, new MapUnlockFieldNotify { FieldId = i });
            fieldId.Add(i);
        }
        

        return Response(MessageId.MapUnlockFieldInfoResponse,new MapUnlockFieldInfoResponse { FieldId = { fieldId } });;
    }
}
