using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDev.Domain.Entities
{
    public class InviteFriends
    {
        public InviteFriends(int userIdInvited, 
            int userIdReceive, 
            bool inviteDenied,
            bool inviteAccept,
            DateTime dateInvite)
        {
            UserIdInvited = userIdInvited;
            UserIdReceive = userIdReceive;
            InviteDenied = inviteDenied;
            InviteAccept = inviteAccept;
            DateInvite = dateInvite;
        }

        public int Id { get; set; }
        public int UserIdInvited { get; set; }
        public int UserIdReceive { get; set; }
        public bool InviteDenied { get; set; }
        public bool InviteAccept { get; set; }
        public DateTime DateInvite { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
