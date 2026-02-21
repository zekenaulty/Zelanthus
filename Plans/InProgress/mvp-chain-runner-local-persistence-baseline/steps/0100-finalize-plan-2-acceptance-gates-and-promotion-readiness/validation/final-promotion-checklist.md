# Plan 2 Final Promotion Checklist

## Scope and Boundary Lock
- [x] Scope and non-goals remain locked to Plan 2 MVP baseline.
- [x] No Postgres, distributed queues, or multi-provider routing added.
- [x] API remains composition-only; no endpoint-surface expansion.

## Contract and Dependency Lock
- [x] StoryEngine project graph follows Decision `0003` dependency direction.
- [x] Required step `prompt_ref` contract is enforced with deterministic failure (`missing_prompt_reference`).
- [x] `workflow_kind` / `chain_mode` mismatch is deterministic failure (`invalid_state_transition`).

## Persistence and Determinism Lock
- [x] Turn artifact path uses `<turn-index>-<step-key>` only.
- [x] Failure artifact naming anchor uses `failure-<turn-index>.json`.
- [x] Counter reservation is monotonic (`next_turn_index`, `next_checkpoint_sequence`) and no-reuse behavior is proven.
- [x] `turn.json` is canonical turn metadata source.
- [x] Atomic temp-write + replace strategy is implemented for local store writes.

## Reason-Code and Resume Lock
- [x] Baseline reason-code constants are pinned in runner contracts.
- [x] Deterministic failure handling emits one primary reason code.
- [x] Cognitive resume restart emits policy reason `cognitive_restart_required`.
- [x] Conversational resume continues from last successful persisted step.
- [x] Continuity-handle invalid path emits `continuity_handle_invalid` without advancing continuity key.

## Proof and Validation Lock
- [x] Plan 2 proof suite added in `ChainRunnerContractProofTests`.
- [x] `dotnet test` deterministic path passes for contract proofs and architecture tests.
- [x] Acceptance evidence matrix is updated with executed test evidence.

## Stop Conditions
- [ ] Any nondeterministic artifact-path behavior detected.
- [ ] Missing canonical turn metadata source.
- [ ] Missing required reason-code mapping evidence.
- [ ] Missing resume-policy proof coverage.

Status: promotion-ready for user review within the active InProgress execution branch.
