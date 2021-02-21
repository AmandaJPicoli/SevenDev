using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDev.Domain.Entities
{
    #region Construtor

    #endregion
    public class InviteFriends
    {
        #region Contrutor
        public InviteFriends(
             int id,
             int userIdInvited,
             int userIdReceive,
             bool inviteDenied,
             bool inviteAccept,
             DateTime dateInvite)
        {
            Id = id;
            UserIdInvited = userIdInvited;
            UserIdReceive = userIdReceive;
            InviteDenied = inviteDenied;
            InviteAccept = inviteAccept;
            DateInvite = dateInvite;
        }
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

        public InviteFriends()
        {
        }
        #endregion

        #region Propriedades
        public int Id { get; set; }
        public int UserIdInvited { get; set; }
        public int UserIdReceive { get; set; }
        public bool InviteDenied { get; set; }
        public bool InviteAccept { get; set; }
        public DateTime DateInvite { get; set; }

        #endregion

        #region Metodos
        public void SetId(int id)
        {
            Id = id;
        }

        public void UpdateInvite(bool accept,
                               bool denied
                               )
        {
            
            InviteAccept = accept;
            InviteDenied = denied;
        }
        #endregion

    }
}
