


USE [RMSDb]
DELETE FROM [dbo].AssignRequisitions;

DBCC CHECKIDENT ('AssignRequisitions', RESEED, 0)

USE [RMSDb]
DELETE FROM [dbo].CancelRequisitions;

DBCC CHECKIDENT ('CancelRequisitions', RESEED, 0)

USE [RMSDb]
DELETE FROM [dbo].[RequisitionStatus];

DBCC CHECKIDENT ('RequisitionStatus', RESEED, 0)

USE [RMSDb]
DELETE FROM [dbo].Notifications;

DBCC CHECKIDENT ('Notifications', RESEED, 0)

USE [RMSDb]
DELETE FROM [dbo].MailServices;

DBCC CHECKIDENT ('MailServices', RESEED, 0)

USE [RMSDb]
DELETE FROM [dbo].Feedbacks;

DBCC CHECKIDENT ('Feedbacks', RESEED, 0)

USE [RMSDb]
DELETE FROM [dbo].RequisitionHistories;

DBCC CHECKIDENT ('RequisitionHistories', RESEED, 0)

USE [RMSDb]
DELETE FROM [dbo].Requisitions;

DBCC CHECKIDENT ('Requisitions', RESEED, 0)