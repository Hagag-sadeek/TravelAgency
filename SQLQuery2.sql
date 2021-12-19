declare @date date =getdate()
-----------------waqf
update  Tickets set price=110 where ticketdate =@date and AppointmentId=4
and supplierid=14
-----------------ciry
update  Tickets set price=100 where ticketdate =@date and AppointmentId=4
and supplierid in (10,16)
-----------------salah
update  Tickets set price=130 where ticketdate =@date and AppointmentId=4
and supplierid   =24
-----------------n2
update  Tickets set price=120 where ticketdate =@date and AppointmentId=4
and supplierid in (17,13)

-----------------cairo
update  Tickets set price=110 where ticketdate =@date and AppointmentId=4
and supplierid=14

