namespace TestSocket
{
    // MAYBE: SEPERATE THE STATE OUT BY CATEGORY (main loop or game room ??)
    public enum State
    {
        NEW,
        RECEIVE_NEW_CONNECTION,
        RECEIVE_ROOM_PACKET, 

        // room packets
        RECEIVE_FROM_LOBBY, 
        RECEIVE_CHOOSE_CHARACTER_INFO,
        RECEIVE_CHOOSE_STAGE_INFO,
        RECEIVE_POSITION 
    
    };
}