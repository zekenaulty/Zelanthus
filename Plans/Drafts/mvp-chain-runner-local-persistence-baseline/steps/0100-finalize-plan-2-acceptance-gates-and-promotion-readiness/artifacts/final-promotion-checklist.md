# Final Promotion Checklist

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0100-finalize-plan-2-acceptance-gates-and-promotion-readiness`

## Required Gates
- [ ] Scope/non-goals are explicit and still correct.
- [ ] Cross-plan dependencies are explicit and valid.
- [ ] Project dependency implementation matrix is explicit and consistent with architecture decisions.
- [ ] Runner contract explicitly supports variable-length `CognitiveChain` and `ConversationalChain` with `PLAN_STEP/EXECUTE` semantics.
- [ ] Minimal workflow abstraction and route-hook seams are explicitly defined.
- [ ] Workflow step contract includes required `prompt_ref` (`prompt_id`, `prompt_version`) for Plan 2 execution.
- [ ] Local persistence contract is explicitly workspace-local and excludes external services.
- [ ] Retry/resume policy includes pinned reason codes and deterministic behavior.
- [ ] Missing/invalid step `prompt_ref` deterministic failure (`missing_prompt_reference`) is pinned and evidenced.
- [ ] Resume policy explicitly differentiates cognitive restart-from-start vs conversational resume-from-last-success.
- [ ] Proof test seed list and evidence spec are explicit and complete.
- [ ] Acceptance evidence matrix maps all DoD points to artifact/test evidence paths.
- [ ] Artifact path contract is deterministic and uses `<turn-index>-<step-key>` only.
- [ ] Canonical turn metadata record (`turn.json`) is explicit and enforced.
- [ ] Run/turn state machine table is explicit, including illegal transition handling.
- [ ] Counter reservation rules for `next_turn_index`/`next_checkpoint_sequence` and persisted `turn_index`/`checkpoint_sequence` are explicit.
- [ ] Crash/partial-write reservation behavior is explicit and forbids index reuse.
- [ ] Minimum atomic write strategy is explicit (`temp in same directory -> flush/fsync -> atomic replace`).
- [ ] Failure artifact naming anchor is explicit (`failure-<turn-index>.json`).
- [ ] Route-hook generated step-key and persisted effective-queue rules are explicit.
- [ ] `route_hook_key` grammar is explicit and guarantees generated step-key path safety.
- [ ] `workflow_kind`/`chain_mode` alignment invariant and deterministic mismatch failure behavior are explicit.
- [ ] Provenance artifact shape is strict superset with deterministic Prompting mapping contract.
- [ ] Continuity-handle behavior is capability-conditioned (supported vs unsupported) and non-contradictory.

## Stop Conditions
- Missing dependency direction assertion coverage.
- Missing reason-code mapping for any retry/terminal scenario.
- Missing cognitive-vs-conversational resume policy differentiation.
- Any implicit reliance on hidden continuity state for correctness.
- Any planned Postgres/distributed runtime work inside Plan 2 scope.
- Any non-deterministic artifact path rule or objective-text path dependency.
- Missing canonical turn metadata authority definition (`turn.json` vs other artifacts).
- Missing explicit step-to-prompt binding contract (`prompt_ref`) for required execution paths.
- Missing deterministic reservation order for run counters (`next_turn_index`, `next_checkpoint_sequence`).
- Missing `workflow_kind`/`chain_mode` alignment rule or mismatch failure behavior.
- Missing explicit route-hook key grammar for generated step-key safety.
- Any index-reuse behavior after interruption/partial-write scenarios.
- Timestamp assertions rely on raw cross-run value equality without deterministic clock injection.
