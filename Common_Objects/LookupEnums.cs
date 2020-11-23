﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Common_Objects
{
    public enum MenuContainerEnum
    {
        MainMenu = 1,
        CPRMenu =  2,
        NisisMenu = 3,
        ACMMenu =  4,
        PCMMenu = 5,
        ADPMenu = 8,
        VEPMenu = 7,
        RACAPMenu =6,
        ReportMenu = 9, 

    }

    public enum ModuleEnum
    {
        Main = 1,
        CPR = 2,
        Nisis = 3,
        ACM = 4,
        PCM = 5,
        ADP = 7,
        VEP = 10,
        RACAP = 8,
        Reporting = 11,
    }

    public enum AcmSection
    {
        Placement = 1,
        Supervision = 2,
        Aftercare = 3
    }

    public enum AddressTypeEnum
    {
        PhysicalAddress = 1,
        PostalAddress = 2
    }

    public enum LookupTableEnum
    {
        AbuseTypes = 1,
        Allergies = 2,
        ChronicIllnesses = 3,
        ClientTypes = 4,
        Disabilities = 5,
        EyeColors = 6,
        Genders = 7,
        Grades = 8,
        HairColors = 9,
        IdenficationTypes = 10,
        IncomeRanges = 11,
        InjuryTypes = 12,
        JobPositions = 13,
        Languages = 14,
        MaritalStatusses = 15,
        Nationalities = 16,
        Occupations = 17,
        OffenceCategories = 18,
        PopulationGroups = 19,
        PreferredContactTypes = 20,
        Races = 21,
        ReceptionActionTypes = 22,
        ReceptionVisitTypes = 23,
        ReferralFocusArea = 24,
        RelationshipTypes = 25,
        Religions = 26,
        SchoolTypes = 27,
        AnotherLookup = 28
    }

    public enum MedicalConditionTypeEnum
    {
        Allergy = 1,
        ChronicIllness = 2,
        Decease = 3
    }

    public enum RelationTypeEnum
    {
        AdoptiveParents = 1,
        BiologicalParents = 2,
        FamilyMembers = 3,
        FosterParents = 4,
        Caregivers = 5,
        ProspectiveAdoptiveParents =6
    }

    public enum ImageTypeEnum
    {
        ProfilePhoto = 1
    }

    public enum YesNoOptionsEnumType
    {
        No = 0,
        Yes = 1
    }

    public enum SchoolTypeEnumType
    {
        FormalInformalSpecialNeeds = 1,
        SchoolECDCentre = 2
    }

    public enum SchoolGradeEnumType
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Eleven = 11,
        Twelve = 12
    }

    public enum GenderEnumType
    {
        Male = 1,
        Female = 2
    }

    #region CPR Enums

    public enum InquiryEmployementType
    {
        Employee = 1,
        [Display(Name = "Potential Employee")]
        [Description("Potential Employee")]
        PotentialEmployee = 2
    }

    public enum RulingSearch
    {
        All = 1,
        Yes = 2,
        No = 3
    }

    public enum IncidentStatusEnum
    {
        New = 1,
        InProgress = 2,
        Closed = 3
    }

    #endregion

    #region NISIS Enums

    public enum QuestionnaireQuestionColumnPopulateByTypeEnum
    {
        GenderFromID = 1,
        AgeFromID = 2,
        BirthdateFromId = 3,
        FirstNameFromParticipant = 4,
        SurnameFromParticipant = 5,
        IDNumberFromParticipant = 6,
        RelationToHeadFromParticipant = 7,
        MaritalStatusFromParticipant = 8
    }

    public enum QuestionnaireQuestionTypeEnum
    {
        Text = 1,
        Radionbutton = 2,
        Dropdown = 3,
        Checkbox = 4,
        Checkboxlist = 5,
        Date = 6,
        DateTime = 7,
        AddRow = 8
    }

    public enum ListingMethodEnum
    {
        NotPerformed = 1,
        OfflineListing = 2,
        PaperListing = 3,
        MobileListing = 4
    }

    public enum ProfilingMethodEnum
    {
        PaperQuestionnaire = 1,
        MobileHandset = 2
    }

    public enum StructureTypeEnum
    {
        Bank = 1,
        BottleStore = 2,
        Business = 3,
        Church = 4,
        DayClinic = 5,
        DemolishedStructure = 6,
        DoesNotExist = 7,
        Factory = 8,
        FillingStation = 9,
        Garage = 10,
        GuestHouse = 11,
        Market = 12,
        Offices = 13,
        Other = 14,
        Sports = 15,
        Park = 16,
        PrivateDwelling = 17,
        PoliceStation = 18,
        PostOffice = 19,
        HolidayHome = 20,
        ResidentialHotel = 21,
        School = 22,
        HomeForTheAged = 23,
        StudentResidence = 24,
        WorkersHostel = 25,
        Shop = 26,
        StorageRoom = 27,
        VacantLand = 28,
        VacantStand = 29
    }

    public enum ProfilingToolTypeEnum
    {
        HouseholdProfiling = 1,
        CommunityProfiling = 2
    }

    public enum QAStatusEnum
    {
        NotSet = 1,
        Approved = 2,
        NotApproved = 3
    }

    public enum ReferralSourceTypeEnum
    {
        Manual = 1,
        WOPReferralEngine = 2
    }

    public enum ServiceStatusEnum
    {
        Created = 1,
        Referred = 2,
        Accepted = 3,
        InProgress = 4,
        Completed = 5,
        Cancelled = 6,
        Rejected = 7,
        Reassigned = 8,
        Deleted = 9,
        OnHold = 10
    }

    public enum ExternalVerificationStatusEnum
    {
        Created = 1,
        Referred = 2,
        Accepted = 3,
        InProgress = 4,
        Completed = 5,
        Cancelled = 6,
        Rejected = 7,
        Reassigned = 8,
        Deleted = 9,
        OnHold = 10
    }

    public enum ReferralPriorityEnum
    {
        Normal = 1,
        High = 2
    }

    #endregion

}
