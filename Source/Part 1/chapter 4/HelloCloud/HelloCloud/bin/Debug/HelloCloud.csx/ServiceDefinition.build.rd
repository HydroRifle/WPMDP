<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="HelloCloud" generation="1" functional="0" release="0" Id="01901687-ed63-4c05-85fa-1c8e2f55f797" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="HelloCloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WCFServiceWebRole1:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/HelloCloud/HelloCloudGroup/LB:WCFServiceWebRole1:HttpIn" />
          </inToChannel>
        </inPort>
        <inPort name="WebRole1:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/HelloCloud/HelloCloudGroup/LB:WebRole1:HttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WCFServiceWebRole1:?IsSimulationEnvironment?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWCFServiceWebRole1:?IsSimulationEnvironment?" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1:?RoleHostDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWCFServiceWebRole1:?RoleHostDebugger?" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1:?StartupTaskDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWCFServiceWebRole1:?StartupTaskDebugger?" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1:DiagnosticsConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWCFServiceWebRole1:DiagnosticsConnectionString" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWCFServiceWebRole1Instances" />
          </maps>
        </aCS>
        <aCS name="WebRole1:?IsSimulationEnvironment?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWebRole1:?IsSimulationEnvironment?" />
          </maps>
        </aCS>
        <aCS name="WebRole1:?RoleHostDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWebRole1:?RoleHostDebugger?" />
          </maps>
        </aCS>
        <aCS name="WebRole1:?StartupTaskDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWebRole1:?StartupTaskDebugger?" />
          </maps>
        </aCS>
        <aCS name="WebRole1:DiagnosticsConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWebRole1:DiagnosticsConnectionString" />
          </maps>
        </aCS>
        <aCS name="WebRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HelloCloud/HelloCloudGroup/MapWebRole1Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WCFServiceWebRole1:HttpIn">
          <toPorts>
            <inPortMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1/HttpIn" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:WebRole1:HttpIn">
          <toPorts>
            <inPortMoniker name="/HelloCloud/HelloCloudGroup/WebRole1/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWCFServiceWebRole1:?IsSimulationEnvironment?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1/?IsSimulationEnvironment?" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1:?RoleHostDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1/?RoleHostDebugger?" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1:?StartupTaskDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1/?StartupTaskDebugger?" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1:DiagnosticsConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1/DiagnosticsConnectionString" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1Instances" />
          </setting>
        </map>
        <map name="MapWebRole1:?IsSimulationEnvironment?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WebRole1/?IsSimulationEnvironment?" />
          </setting>
        </map>
        <map name="MapWebRole1:?RoleHostDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WebRole1/?RoleHostDebugger?" />
          </setting>
        </map>
        <map name="MapWebRole1:?StartupTaskDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WebRole1/?StartupTaskDebugger?" />
          </setting>
        </map>
        <map name="MapWebRole1:DiagnosticsConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloCloud/HelloCloudGroup/WebRole1/DiagnosticsConnectionString" />
          </setting>
        </map>
        <map name="MapWebRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HelloCloud/HelloCloudGroup/WebRole1Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WCFServiceWebRole1" generation="1" functional="0" release="0" software="D:\Project\WP Mango Programming Practice\Part 1\chapter 4\HelloCloud\HelloCloud\bin\Debug\HelloCloud.csx\roles\WCFServiceWebRole1" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="8081" />
            </componentports>
            <settings>
              <aCS name="?IsSimulationEnvironment?" defaultValue="" />
              <aCS name="?RoleHostDebugger?" defaultValue="" />
              <aCS name="?StartupTaskDebugger?" defaultValue="" />
              <aCS name="DiagnosticsConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WCFServiceWebRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WCFServiceWebRole1&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WebRole1&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1Instances" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WebRole1" generation="1" functional="0" release="0" software="D:\Project\WP Mango Programming Practice\Part 1\chapter 4\HelloCloud\HelloCloud\bin\Debug\HelloCloud.csx\roles\WebRole1" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="?IsSimulationEnvironment?" defaultValue="" />
              <aCS name="?RoleHostDebugger?" defaultValue="" />
              <aCS name="?StartupTaskDebugger?" defaultValue="" />
              <aCS name="DiagnosticsConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WebRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WCFServiceWebRole1&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WebRole1&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HelloCloud/HelloCloudGroup/WebRole1Instances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="WCFServiceWebRole1Instances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WebRole1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="668c5426-84bd-42ad-8be8-f501a1c9b0f9" ref="Microsoft.RedDog.Contract\ServiceContract\HelloCloudContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="e4310779-c709-4c6a-8ef2-c3831ccfea69" ref="Microsoft.RedDog.Contract\Interface\WCFServiceWebRole1:HttpIn@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/HelloCloud/HelloCloudGroup/WCFServiceWebRole1:HttpIn" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="efdf609b-5db8-432e-b0e9-65c2280bf900" ref="Microsoft.RedDog.Contract\Interface\WebRole1:HttpIn@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/HelloCloud/HelloCloudGroup/WebRole1:HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>