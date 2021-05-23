namespace Unity.MemoryProfiler.Editor.Format.QueriedSnapshot
{
    public enum EntryType : ushort
    {
        Metadata_Version = 0,
        Metadata_RecordDate,
        Metadata_UserMetadata,
        Metadata_CaptureFlags,
        Metadata_VirtualMachineInformation,
        NativeTypes_Name,
        NativeTypes_NativeBaseTypeArrayIndex,
        NativeObjects_NativeTypeArrayIndex,
        NativeObjects_HideFlags,
        NativeObjects_Flags,
        NativeObjects_InstanceId,
        NativeObjects_Name,
        NativeObjects_NativeObjectAddress,
        NativeObjects_Size,
        NativeObjects_RootReferenceId,
        GCHandles_Target,
        Connections_From,
        Connections_To,
        ManagedHeapSections_StartAddress,
        ManagedHeapSections_Bytes,
        ManagedStacks_StartAddress,
        ManagedStacks_Bytes,
        TypeDescriptions_Flags,
        TypeDescriptions_Name,
        TypeDescriptions_Assembly,
        TypeDescriptions_FieldIndices,
        TypeDescriptions_StaticFieldBytes,
        TypeDescriptions_BaseOrElementTypeIndex,
        TypeDescriptions_Size,
        TypeDescriptions_TypeInfoAddress,
        TypeDescriptions_TypeIndex,
        FieldDescriptions_Offset,
        FieldDescriptions_TypeIndex,
        FieldDescriptions_Name,
        FieldDescriptions_IsStatic,
        NativeRootReferences_Id,
        NativeRootReferences_AreaName,
        NativeRootReferences_ObjectName,
        NativeRootReferences_AccumulatedSize,
        NativeAllocations_MemoryRegionIndex,
        NativeAllocations_RootReferenceId,
        NativeAllocations_AllocationSiteId,
        NativeAllocations_Address,
        NativeAllocations_Size,
        NativeAllocations_OverheadSize,
        NativeAllocations_PaddingSize,
        NativeMemoryRegions_Name,
        NativeMemoryRegions_ParentIndex,
        NativeMemoryRegions_AddressBase,
        NativeMemoryRegions_AddressSize,
        NativeMemoryRegions_FirstAllocationIndex,
        NativeMemoryRegions_NumAllocations,
        NativeMemoryLabels_Name,
        NativeAllocationSites_Id,
        NativeAllocationSites_MemoryLabelIndex,
        NativeAllocationSites_CallstackSymbols,
        NativeCallstackSymbol_Symbol,
        NativeCallstackSymbol_ReadableStackTrace,
        NativeObjects_GCHandleIndex,
        Count, //used to keep track of entry count, only add c++ matching entries above this one
    }
}
