   M S B A M L   `   `   `  ����G  CFrontDeskApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FrontDeskAppFrontDeskApp  5   	       FrontDeskApp.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FrontDeskApp     5   	     � Title$  
MainWindow��5   	   $	��450��6   $
��1080��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��10q�6"   .@�5      �� $��	All Rooms��6   .��5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��10q�6:   .+�5      �� $��Room Number: ��6$   $��Center=�69   �� 5      -   5         ��Name$ 	searchBox��5   "   $	��150��65   $��0,0,10,0q�6A   �� 5      -   5         ��Name$ searchButton��5   !   $��Search��67   6H   5      �� 5      -   5         ��Name$ roomList��5      $
��1080��6I   ��61   ��  �� View 5      �� 5      .P�5   "   ��  �� Width$ 75��61   $��Room Nr.��6<    �� DisplayMemberBinding 6N   �� *
RoomNumber+�� 5   "   $	 100��61   $��	Room type��6=    6P   �� *
RoomType+�� 5   "   $	 100��61   $��Number of beds��6=    6U   �� *	NumBeds+�� 5   "   $ 50��61   $��Price��6<    6K   �� *Price+�� 5   "   $	 160��61   $��Available from��6=    6U   �� *AvailableFrom+�� 5   "   $	 160��61   $��Available to��6=    6S   �� *AvailableTo+�� 5    "   $	 100��61   $��Booked��6=    6M   �� *Booked+�� 5!   "   $	 100��61   $��	Last name��6=    6P   �� *CustomerLastName+�� 5"   "   $	 100��61   $��
First name��6=    6Q   �� *CustomerFirstName+�� 5#   "   $	 100��61   $��Check in date��6=    6T   �� *CheckInDate+5$      5&      5'      5(      �� 5*      $��Bookings��6   .��5+      �  5,      �� 5-      $
��Auto �6(   �� 5.      $��* �6(   .+�50      �� $ 
Horizontal=�6!   $��10q�6:   .+�51      �� $��Last Name: ��6$   $��Center=�67   �� 52      -   5        $ bookingSearch��52   "   $	��150��69   $��0,0,10,0q�6E   �� 53      -   5        $ bookingSearchButton��53   !   $��Search booking��6>   6W   54      �� 55      -   5        $��bookingList��55      $��1��6J   ��62   ��  56      �� 57      .P�58   "   �� $	 125��61   $��	Last name��6=    6P   �� 	 �� Path$	 LastName��� 59   "   $	 125��61   $��
First name��6=    6Q   �� $	 	FirstName��� 5:   "   $	 125��61   $��Email��6=    6L   �� $	 Email��� 5;   "   $	 125��61   $��Room number��6=    6R   �� $	 
RoomNumber��� 5<   "   $	 125��61   $��Check in date��6=    6T   �� $	 CheckInDate��� 5=   "   $	 125��61   $��Check out date��6=    6U   �� $	 CheckOutDate�5>      5@      5A      5B      �� 5D      $��
Swap rooms��6   .��5E      �� $��10q�6   .+�5F      �� $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5G      �� $��Customer Last Name:��6$   $	��125��6?   $��Center=�6K   �� 5H      -   5        $ customerLastNameTextBox��5H   "   $	��200��6C   5I      �� 5J      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5K      �� $��Old room number:��6$   $	��125��6<   $��Center=�6H   �� 5L      -   5        $ oldRoomNumberTextBox��5L   "   $	��200��6@   5M      �� 5N      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5O      �� $��New room number:��6$   $	��125��6<   $��Center=�6H   �� 5P      -	   5        $ newRoomNumberTextBox��5P   "   $	��200��6@   5Q      �� 5R      -
   5        $ changeRoomButton��5R      $��Change room booking��67   $	��150��6t   $
��Left=�6�   $��10 5 0 0q�6�   �� 5S      -   5        
 ��Name$
 roomChangeResult��5S       $��0 10 0 0q�6:   5T      5U      �� 5W      $��New Reservation��6   .��5X      �� $��10q�6   .+�5Y      �� $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5Z      �� $��Customer First Name:��6$   $	��125��6@   $��Center=�6L   �� 5[      -   5        $ customerFirstNameTextBox��5[   "   $	��200��6D   5\      �� 5]      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5^      �� $��Customer Last Name:��6$   $	��125��6?   $��Center=�6K   �� 5_      -   5        $$ customerLastNameTextBoxBooking��5_   "   $	��200��6J   5`      �� 5a      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5b      �� $��Customer Email:��6$   $	��125��6;   $��Center=�6G   �� 5c      -   5        $ customerEmailTextBox��5c   "   $	��200��6@   5d      �� 5e      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5f      �� $��Room Number:��6$   $	��125��68   $��Center=�6D   �� 5g      -   5        $ roomNumberTextBox��5g   "   $	��200��6=   5h      �� 5i      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5j      �� $��Check In Date:��6$   $	��125��6:   $��Center=�6F   (  "System.Windows.Controls.DatePicker  5k      -   5          Name$ checkInDatePicker��5k   %   $	��200��6@   5l      �� 5m      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5n      �� $��Check Out Date:��6$   $	��125��6;   $��Center=�6G     5o      -   5        $ checkOutDatePicker��5o   %   $	��200��6A   5p      �� 5q      -   5        $ addReservationButton��5q      $��Add Reservation��6;   $	��150��6x   $
��Left=�6�   $��10 5 0 0q�6�   �� 5r      -   5        $
 addReservationResult��5r       $��0 10 0 0q�6>   5s      5t      �� 5v      $��Delete reservation��6   .��5w      �� $��10q�6   .+�5x      �� $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5y      �� $��Customer Last Name:��6$   $	��125��6?   $��Center=�6K   �� 5z      -   5        $# deleteCustomerLastNameTextBox��5z   "   $	��200��6I   5{      �� 5|      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5}      �� $��Room number:��6$   $	��125��68   $��Center=�6D   �� 5~      -   5        $ deleteRoomNumberTextBox��5~   "   $	��200��6C   5      �� 5�      $ 
Horizontal=�6!   $��0 0 0 10q�6:   .+�5�      �� $��Check-in date:��6$   $	��125��6:   $��Center=�6F     5�      -   5        $ deleteCheckInDatePicker��5�   %   $	��200��6F   5�      �� 5�      -   5        $ deleteReservationButton��5�      $��Delete reservation��6>   $	��150��6�   $
��Left=�6�   $��10 5 0 0q�6�   �� 5�      -   5        $
 deleteReservationResult��5�       $��0 10 0 0q�6A   5�      5�      �� 5�      $��Check In/Out��6   .��5�      �  5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $ 
Horizontal=�6!   $��10q�6:   .+�5�      �� $��Customer Last Name: ��6$   $��Center=�6@   $��0,0,10,0q�6[   �� 5�      -   5        $' !checkinoutCustomerLastNameTextBox��5�   "   $	��150��6M   $��0,0,10,0q�6Y   5�      �� 5�      -   5        $ checkinButton��5�      $��Check In��64   $��75��6G   $��1��6R   $
��Left=�6_   $��
150,0,10,0q�6z   6�   �� 5�      -   5        $ checkoutButton��5�      $��	Check Out��65   $��75��6I   $��2��6T   $
��Left=�6a   $��
150,5,10,0q�6|   6�   �� 5�      -   5        $
 checkinoutStatusTextBlock��5�       $�� ��6C   $��3��6K   $��10q�6X   5�      5�      �� 5�      $��Service Requests��6   .��5�      �  5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $ 
Horizontal=�6!   $��10q�6:   .+�5�      �� $��Room Number:��6$   $��Center=�68   $��5 0 5 0q�6S   �� 5�      -   5        $ serviceRoomNumberTextBox��5�   "   $��Center=�6D   $	��100��6_   �� 5�      $��Service Type:��6$   $��Center=�69   $��5 0 5 0q�6T   �� 5�      -   5        $ serviceTypeTextBox��5�   "   $��Center=�6>   $	��100��6Y   �� 5�      $��Description:��6$   $��Center=�68   $��5 0 5 0q�6S   �� 5�      -   5        $ serviceDescriptionTextBox��5�   "   $��Center=�6E   $	��200��6`   �� 5�      -    5        $ addServiceButton��5�   !   $��Add Service Request��6;   $��5q�6Y   $
��Left=�6d   6   5�      �� 5�      -!   5        $
 serviceFeedbackTextBlock��5�       $��0,45,20,298q�6B   $��2��6W   &   System.Windows.Controls.DataGrid  5�      -"   5          Name$ serviceRequestsDataGrid��5�      $��1��6@   $��
0,43,10,10q�6M      AutoGenerateColumns$ False��6a      Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Room Number��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *
RoomNumber+  5�      $ Service Type��61    6G   �� *ServiceType+  5�      $ Description��61    6F   �� *Description+  5�      $ Date Requested��61    6I   �� *DateRequested+  5�      $ Date Completed��61    6I   �� *DateCompleted+5�      5�      5�      5�      5�      5�      