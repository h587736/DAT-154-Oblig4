Using System;
Namespace Library
{
    Partial Public Class Booking
    {
        Public int Id { Get; Set; }
        Public int CustomerId { Get; Set; }
        Public Customer Customer { Get; Set; }
        Public int RoomId { Get; Set; }
        Public Rooms Room { Get; Set; }
        Public DateTime CheckInDate { Get; Set; }
        Public DateTime CheckOutDate { Get; Set; }
    }
}
