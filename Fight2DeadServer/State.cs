namespace TestSocket
{
    public enum State
    {
        NEW,
        RECEIVE_NEW_CONNECTION,
        RECEIVE_ROOM_PACKET, 
        RECEIVE_FROM_LOBBY, 
        RECEIVE_POSITION 
    
    };
}