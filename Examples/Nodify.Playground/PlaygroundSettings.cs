﻿using Nodify.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nodify.Playground;

public class PlaygroundSettings : ObservableObject
{
    public IReadOnlyCollection<ISettingViewModel> Settings { get; }

    private PlaygroundSettings()
    {
        Settings = new ObservableCollection<ISettingViewModel>()
        {
            new ProxySettingViewModel<bool>(
                () => Instance.ShowGridLines,
                val => Instance.ShowGridLines = val,
                "Show grid lines:"),
            new ProxySettingViewModel<bool>(
                () => Instance.ShouldConnectNodes,
                val => Instance.ShouldConnectNodes = val,
                "Connect nodes:"),
            new ProxySettingViewModel<bool>(
                () => Instance.AsyncLoading,
                val => Instance.AsyncLoading = val,
                "Async loading:"),
            new ProxySettingViewModel<bool>(
                () => Instance.UseCustomConnectors,
                val => Instance.UseCustomConnectors = val,
                "Custom connectors:"),
            new ProxySettingViewModel<uint>(
                () => Instance.MinNodes,
                val => Instance.MinNodes = val,
                "Min nodes:"),
            new ProxySettingViewModel<uint>(
                () => Instance.MaxNodes,
                val => Instance.MaxNodes = val,
                "Max nodes:"),
            new ProxySettingViewModel<uint>(
                () => Instance.MinConnectors,
                val => Instance.MinConnectors = val,
                "Min connectors:"),
            new ProxySettingViewModel<uint>(
                () => Instance.MaxConnectors,
                val => Instance.MaxConnectors = val,
                "Max connectors:"),
            new ProxySettingViewModel<uint>(
                () => Instance.PerformanceTestNodes,
                val => Instance.PerformanceTestNodes = val,
                "Performance test nodes:"),
        };
    }

    public static PlaygroundSettings Instance { get; } = new PlaygroundSettings();

    private bool _shouldConnectNodes = true;
    public bool ShouldConnectNodes
    {
        get => _shouldConnectNodes;
        set => SetProperty(ref _shouldConnectNodes, value);
    }

    private bool _asyncLoading = true;
    public bool AsyncLoading
    {
        get => _asyncLoading;
        set => SetProperty(ref _asyncLoading, value);
    }

    private uint _minNodes = 10;
    public uint MinNodes
    {
        get => _minNodes;
        set => SetProperty(ref _minNodes, value)
            .Then(() => MaxNodes = MaxNodes < MinNodes ? MinNodes : MaxNodes);
    }

    private uint _maxNodes = 100;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2011:Avoid infinite recursion", Justification = "<Pending>")]
    public uint MaxNodes
    {
        get => _maxNodes;
        set => SetProperty(ref _maxNodes, value)
            .Then(() => MaxNodes = MaxNodes < MinNodes ? MinNodes : MaxNodes);
    }

    private uint _minConnectors = 0;
    public uint MinConnectors
    {
        get => _minConnectors;
        set => SetProperty(ref _minConnectors, value)
            .Then(() => MaxConnectors = MaxConnectors < MinConnectors ? MinConnectors : MaxConnectors);
    }

    private uint _maxConnectors = 4;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2011:Avoid infinite recursion", Justification = "<Pending>")]
    public uint MaxConnectors
    {
        get => _maxConnectors;
        set => SetProperty(ref _maxConnectors, value)
            .Then(() => MaxConnectors = MaxConnectors < MinConnectors ? MinConnectors : MaxConnectors);
    }

    private uint _performanceTestNodes = 1000;
    public uint PerformanceTestNodes
    {
        get => _performanceTestNodes;
        set => SetProperty(ref _performanceTestNodes, value);
    }

    private bool _showGridLines = true;
    public bool ShowGridLines
    {
        get => _showGridLines;
        set => SetProperty(ref _showGridLines, value);
    }

    private bool _customConnectors = true;
    public bool UseCustomConnectors
    {
        get => _customConnectors;
        set => SetProperty(ref _customConnectors, value);
    }
}
