-- INVENTORY DATA
INSERT INTO INVENTORY VALUES (N'Kho Tổng');
-- CATEGORY DATA
INSERT INTO CATEGORY VALUES ('IPhone',GETDATE(),NULL,NULL,NULL,'True','False');
INSERT INTO CATEGORY VALUES ('Mac',GETDATE(),NULL,NULL,NULL, 'True','False');
INSERT INTO CATEGORY VALUES ('Ipad',GETDATE(),NULL,NULL,NULL, 'True','False');
INSERT INTO CATEGORY VALUES ('Watch',GETDATE(),NULL,NULL,NULL, 'True','False');
INSERT INTO CATEGORY VALUES (N'Âm thanh',GETDATE(),NULL,NULL,NULL, 'True','False');
-- IPHONE 15 PRO MAX
INSERT INTO PRODUCT VALUES (N'Điện thoại iPhone 15 Plus (128GB) - Chính hãng VN/A', '128 GB', 26590000, 24590000,'Detail product',GETDATE(),NULL,NULL,NULL,'True', 'False',1,'/Images/Products/iphone-15-plus-blue.png');
INSERT INTO PRODUCT VALUES (N'Điện thoại iPhone 15 Plus (256GB) - Chính hãng VN/A', '256 GB', 29590000, 27590000,'Detail product',GETDATE(),NULL,NULL,NULL,'True', 'False',1,'/Images/Products/iphone-15-plus-pink.png');
INSERT INTO PRODUCT VALUES (N'Điện thoại iPhone 15 Pro (512GB) - Chính hãng VN/A', '512 GB', 38490000, 36490000,'Detail product',GETDATE(),NULL,NULL,NULL,'True', 'False',1,'/Images/Products/test.png');
-- IPAD PRO
INSERT INTO PRODUCT VALUES (N'Máy tính bảng iPad Pro M2 11" 5G (128GB) - Chính hãng Apple Việt Nam', '128 GB', 26590000, 23590000,'Detail product',GETDATE(),NULL,NULL,NULL,'True', 'False',3,'/Images/Products/test2.png');
INSERT INTO PRODUCT VALUES (N'Máy tính bảng iPad Pro M2 11" 5G (256GB) - Chính hãng Apple Việt Nam', '256 GB', 28990000, 26990000,'Detail product',GETDATE(),NULL,NULL,NULL,'True', 'False',3,'/Images/Products/test2.png');
INSERT INTO PRODUCT VALUES (N'Máy tính bảng iPad Pro M2 11" 5G (512GB) - Chính hãng Apple Việt Nam', '512 GB', 36990000, 33990000,'Detail product',GETDATE(),NULL,NULL,NULL,'True', 'False',3,'/Images/Products/test2.png');
--SEED ROLE
INSERT INTO [ROLE] VALUES ('User');
INSERT INTO [ROLE] VALUES ('Admin');
-- SEED ACCOUNT
INSERT INTO ACCOUNT VALUES ('Admin','123456','Admin@gmail.com','ADMIN','06 Vu Trong Phung','0909000999','01-01-1998',NULL,GETDATE(),NULL,NULL,'False','True','False',2);
INSERT INTO ACCOUNT VALUES ('User1','123456','User1@gmail.com','USER1','05 Vu Trong Phung','0909000888','02-01-1998',NULL,GETDATE(),NULL,NULL,'False','True','False',1);
INSERT INTO ACCOUNT VALUES ('User2','123456','User2@gmail.com','USER2','04 Vu Trong Phung','0909000777','03-01-1998',NULL,GETDATE(),NULL,NULL,'True','True','False',1);
INSERT INTO ACCOUNT VALUES ('User3','123456','User3@gmail.com','USER3','03 Vu Trong Phung','0909000666','04-01-1998',NULL,GETDATE(),NULL,NULL,'False','False','False',1);
INSERT INTO ACCOUNT VALUES ('User4','123456','User4@gmail.com','USER4','02 Vu Trong Phung','0909000555','05-01-1998',NULL,GETDATE(),NULL,NULL,'False','True','True',1);

SELECT * FROM [ORDER]
SELECT * FROM [ORDERDETAIL]
INSERT INTO [ORDER] VALUES (24590000,GETDATE(),NULL,'User1@gmail.com',NULL,N'01 Nguyễn Hữu Thọ',NULL,N'Đã đặt hàng','False',2);
INSERT INTO [ORDERDETAIL] VALUES (1,1,24590000,1);
INSERT INTO [ORDER] VALUES (52180000,GETDATE(),NULL,'User1@gmail.com',NULL,N'01 Nguyễn Hữu Thọ',NULL,N'Đã đặt hàng','False',2);
INSERT INTO [ORDERDETAIL] VALUES (1,1,24590000,2);
INSERT INTO [ORDERDETAIL] VALUES (2,1,27590000,2);
---
INSERT INTO [ORDER] VALUES (24590000,GETDATE(),NULL,'User1@gmail.com',NULL,N'01 Nguyễn Hữu Thọ',NULL,N'Đã đặt hàng','False',2);
INSERT INTO [ORDERDETAIL] VALUES (1,1,24590000,3);
INSERT INTO [ORDER] VALUES (24590000,GETDATE(),NULL,'User1@gmail.com',NULL,N'01 Nguyễn Hữu Thọ',NULL,N'Đã đặt hàng','False',2);
INSERT INTO [ORDERDETAIL] VALUES (1,1,24590000,4);
INSERT INTO [ORDER] VALUES (24590000,GETDATE(),NULL,'User1@gmail.com',NULL,N'01 Nguyễn Hữu Thọ',NULL,N'Đã đặt hàng','False',2);
INSERT INTO [ORDERDETAIL] VALUES (1,1,24590000,5);
GO