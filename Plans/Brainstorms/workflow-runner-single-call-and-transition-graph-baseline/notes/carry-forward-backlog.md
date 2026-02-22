# Carry-Forward Backlog (Plan-Scoped)

## Purpose
- Prevent loss of high-value workflow hardening work that is currently codified in proof tests but not yet defined as full runtime process policy.

## Interim Policy
- Until a workspace-wide backlog workflow is formally defined, this brainstorm uses a plan-scoped carry-forward backlog note as the temporary source of truth.
- Any unresolved hardening item in this list must be mapped into Draft planning before implementation work begins.

## Active Items
- `WF-BL-001`
  - Item: Harden `cognitive_chain` and `conversational_chain` behavior from proof-oriented contract validation into concrete, documented runtime process policy.
  - Current State: Contract proofs exist (`Tests/Zelanthus.WorkflowContractProofs.Tests/ChainRunnerContractProofTests.cs`) but full end-to-end operational process semantics are not yet fully codified as implementation doctrine.
  - Owner Scope: `StoryEngine runner planning`
  - Trigger for Promotion: Next Draft that extends node execution modes, transition behavior, or runner orchestration policy.
  - Exit Criteria: Runtime process contract, implementation touchpoints, and validation evidence are documented and accepted.

## Rule for New Execution Modes
- Any newly introduced execution mode or transition behavior in planning must add an entry here if proof contracts can exist before full runtime process hardening is complete.
- Do not close an entry without explicit validation evidence in a completed plan package.
