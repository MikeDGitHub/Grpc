using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Greeter;
using Grpc.Core;

namespace GrpcService
{
    [Class]
  public  class GreeterImpl: IMService.IMServiceBase
    {
       public override Task<GroupResult> AddMember(AddMembers request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }
        [Method]
       public override Task<GroupResult> Create(CreateGroup request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult() {GroupId = "1",Stauts = true});
       }

       public override Task<GroupResult> Delete(GroupId request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }

       public override Task<GroupResult> Out(OutGroup request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }

       public override Task<GroupResult> RemoveMember(RemoveMembers request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }

       public override Task<GroupResult> SendGroup(Groups request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }

       public override Task<GroupResult> SendPeer(Peers request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }

       public override Task<GroupResult> SendSystem(Systems request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }

       public override Task<GroupResult> UpdateName(UpdateGroupName request, ServerCallContext context)
       {
           return Task.FromResult(new GroupResult());
       }
    }
}
